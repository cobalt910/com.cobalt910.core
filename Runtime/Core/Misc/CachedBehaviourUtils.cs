namespace com.cobalt910.core.Runtime.Core.Misc
{
    public static class CachedBehaviourUtils
    {
        public static bool InstanceIdEquals(this CachedBehaviour o, CachedBehaviour obj)
        {
            return o.InstanceId == obj.InstanceId;
        }
    }
}