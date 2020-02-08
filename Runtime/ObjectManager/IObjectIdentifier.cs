using com.cobalt910.core.Runtime.PoolManager;

namespace com.cobalt910.core.Runtime.ObjectManager
{
    public interface IObjectIdentifier
    {
        int FamilyId { get; }
        int ObjectId { get; }
        void RegisterComponents(ObjectLocator objectLocator);
    }
}