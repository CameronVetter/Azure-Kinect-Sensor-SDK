using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Microsoft.Azure.Kinect.Sensor;

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1602 // Enumeration items should be documented
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1611 // Element parameters should be documented

namespace Microsoft.Azure.Kinect.BodyTracking
{
    public class BodyFrame : IDisposable
    {
        public const byte BodyIndexMapBackground = 255;

        private readonly object lockBodyFrame = new object();

        private NativeMethods.k4abt_frame_t handle;

        private bool disposedValue; // To detect redundant calls

        internal BodyFrame(NativeMethods.k4abt_frame_t handle)
        {
            // Hook the native allocator and register this object.
            // .Dispose() will be called on this object when the allocator is shut down.
            Allocator.Singleton.RegisterForDisposal(this);

            this.handle = handle;
        }

        ~BodyFrame()
        {
            this.Dispose(false);
        }

        public enum JointId
        {
            Pelvis = 0,
            SpineNaval,
            SpineChest,
            Neck,
            ClaviceLeft,
            ShoulderLeft,
            ElbowLeft,
            WristLeft,
            KHandLeft,
            HandTipLeft,
            ThumbLeft,
            ClaviceRight,
            ShoulderRight,
            ElbowRight,
            WristRight,
            HandRight,
            HandtipRight,
            ThumbRight,
            HipLeft,
            KneeLeft,
            AnkleLeft,
            FootLeft,
            HipRight,
            KneeRight,
            AnkleRight,
            FootRight,
            Head,
            Nose,
            EyeLeft,
            EarLeft,
            EyeRight,
            EarRight,
            Count,
        }

        public TimeSpan Timestamp
        {
            get
            {
                lock (this.lockBodyFrame)
                {
                    if (this.disposedValue)
                    {
                        throw new ObjectDisposedException(nameof(BodyFrame));
                    }

                    ulong timestamp = NativeMethods.k4abt_frame_get_device_timestamp_usec(this.handle);
                    return TimeSpan.FromTicks(checked((long)timestamp) * 10);
                }
            }
        }

        [CLSCompliant(false)]
        public uint GetNumBodies()
        {
            return NativeMethods.k4abt_frame_get_num_bodies(this.handle);
        }

        [CLSCompliant(false)]
        public Skeleton GetBodySkeleton(uint index)
        {
            lock (this.lockBodyFrame)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyTracker));
                }

                Skeleton skeleton = default;
                AzureKinectException.ThrowIfNotSuccess(() => NativeMethods.k4abt_frame_get_body_skeleton(this.handle, index, out skeleton));

                return skeleton;
            }
        }

        [CLSCompliant(false)]
        public uint GetBodyID(uint index)
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(BodyTracker));
            }

            return NativeMethods.k4abt_frame_get_body_id(this.handle, index);
        }

        public Image GetBodyIndexMap()
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(BodyTracker));
            }

            return new Image(NativeMethods.k4abt_frame_get_body_index_map(this.handle));
        }

        public Capture GetCapture()
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(BodyTracker));
            }

            return new Capture(NativeMethods.k4abt_frame_get_capture(this.handle));
        }

        public BodyFrame Reference()
        {
            lock (this.lockBodyFrame)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyFrame));
                }

                return new BodyFrame(this.handle.DuplicateReference());
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue && disposing)
            {
                Allocator.Singleton.UnregisterForDisposal(this);

                this.handle.Close();
                this.handle = null;

                this.disposedValue = true;
            }
        }
    }
}