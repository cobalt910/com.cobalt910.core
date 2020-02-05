using UnityEngine;

namespace ProjectCore.Misc
{
    public static class Vector2Utils
    {
        public static float Random(this Vector2 v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }
    }
}