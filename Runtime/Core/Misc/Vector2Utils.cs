using UnityEngine;

namespace com.cobalt910.core.Runtime.Core.Misc
{
    public static class Vector2Utils
    {
        public static float Random(this Vector2 v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }
    }
}