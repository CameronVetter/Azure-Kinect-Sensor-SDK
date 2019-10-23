using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Microsoft.Azure.Kinect.BodyTracking;

namespace Microsoft.Azure.Kinect.Sensor
{
    [StructLayout(LayoutKind.Sequential)]
    [Sensor.Native.NativeReference("k4abt_body_t")]
    public struct Body
    {
        [CLSCompliant(false)]
        public uint Id;

        public Skeleton Skeleton;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Sensor.Native.NativeReference("k4abt_joint_t")]
    public struct Joint
    {
        public Vector3 Position;

        public Quaternion Orientation;

        public JointConfidenceLevel ConfidenceLevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Sensor.Native.NativeReference("k4abt_skeleton_t")]
    public struct Skeleton
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = (int)BodyFrame.JointId.Count)]
        public Joint[] Joints;
    }
}
