namespace ProjectCore.Misc
{
    public static class CachedBehaviourUtils
    {
        public static bool InstanceIdEquals(this CachedBehaviour o, CachedBehaviour obj)
        {
            return o.InstanceId == obj.InstanceId;
        }
    }
}