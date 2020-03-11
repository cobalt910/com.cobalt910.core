using UnityEngine;

namespace com.cobalt910.core.Runtime.Extension
{
    public static class UnityObjectExtension
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

        public static bool InstanceIdEquals(this Object o, Object obj)
        {
            return o.GetInstanceID() == obj.GetInstanceID();
        }
    }
}