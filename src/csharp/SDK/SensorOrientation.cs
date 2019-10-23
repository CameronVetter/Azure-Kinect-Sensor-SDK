using Microsoft.Azure.Kinect.Sensor.Native;

namespace Microsoft.Azure.Kinect.Sensor
{
    [NativeReference]
    public enum SensorOrientation
    {
        DEFAULT = 0,
        CLOCKWISE90,
        COUNTERCLOCKWISE90,
        FLIP180,
    }
}
