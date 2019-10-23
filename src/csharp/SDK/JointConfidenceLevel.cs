namespace Microsoft.Azure.Kinect.Sensor
{
    public enum JointConfidenceLevel
    {
        K4abtJointConfidenceNone = 0,          /* The joint is out of range (too far from depth camera) */
        K4abtJointConfidenceLow = 1,           /* The joint is not observed (likely due to occlusion), predicted joint pose */
        K4abtJointConfidenceMedium = 2,        /* Medium confidence in joint pose. Current SDK will only provide joints up to this confidence level */
        K4abtJointConfidenceHigh = 3,          /* High confidence in joint pose. Placeholder for future SDK */
        Count,                             /* The total number of confidence levels. */
    }
}
