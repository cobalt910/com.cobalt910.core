using com.cobalt910.core.Runtime.Misc;

namespace com.cobalt910.core.Runtime.Extension
{
    public static class CachedBehaviourExtension
    {
        public static bool InstanceIdEquals(this CachedBehaviour o, CachedBehaviour obj)
        {
            return o.InstanceId == obj.InstanceId;
        }
    }
}