using System.Runtime.InteropServices;
using static Microsoft.Azure.Kinect.BodyTracking.BodyTracker;

namespace Microsoft.Azure.Kinect.Sensor
{
    [StructLayout(LayoutKind.Sequential)]
    [Sensor.Native.NativeReference("k4abt_tracker_configuration_t")]
    public struct BodyTrackerConfiguration
    {
        public SensorOrientation SensorOrientation;

        [MarshalAs(UnmanagedType.I1)]
        public bool CpuOnly;
    }
}
