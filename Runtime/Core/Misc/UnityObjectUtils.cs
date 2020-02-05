using UnityEngine;

namespace ProjectCore.Misc
{
    public static class UnityObjectUtils
    {
        public static bool IsNull(this Object o)
        {
            return ReferenceEquals(o, null);
        }

        public static bool IsNotNull(this Object o)
        {
            return !ReferenceEquals(o, null);
        }

        public static bool RefEquals(this Object o, Object obj)
        {
            return ReferenceEquals(o, obj);
        }
    }
}