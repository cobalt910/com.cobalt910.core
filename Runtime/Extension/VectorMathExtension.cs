using UnityEngine;

namespace com.cobalt910.core.Runtime.Extension
{
    /// Extension methods for Vector4
    public static class Vector4Extension
    {
        public static Quaternion ToQuaternion(this Vector4 v)
        {
            return new Quaternion(v.x, v.y, v.z, v.w);
        }

        public static Quaternion ToNormalizedQuaternion(this Vector4 v)
        {
            v = Vector4.Normalize(v);
            return new Quaternion(v.x, v.y, v.z, v.w);
        }
    }

    /// Extension methods for Quaternion
    public static class QuaternionExtension
    {
        public static Vector4 ToVector4(this Quaternion q)
        {
            return new Vector4(q.x, q.y, q.z, q.w);
        }
    }
}
