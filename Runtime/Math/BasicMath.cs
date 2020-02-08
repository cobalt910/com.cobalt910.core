using UnityEngine;

namespace com.cobalt910.core.Runtime.Math
{
    public static class BasicMath
    {
        public static float Lerp(float a, float b, float mix)
        {
            return a * (1 - mix) + b * mix;
        }
        public static Vector3 Lerp(Vector3 a, Vector3 b, float mix)
        {
            return a * (1 - mix) + b * mix;
        }
    }
}

