using UnityEngine;

namespace com.cobalt910.core.Runtime.Extension
{
    public static class Vector2Extension
    {
        public static float Random(this Vector2 v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }
    }
}