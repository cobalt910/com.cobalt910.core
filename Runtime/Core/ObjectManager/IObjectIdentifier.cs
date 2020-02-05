using com.cobalt910.core.Runtime.Core.PoolManager;

namespace com.cobalt910.core.Runtime.Core.ObjectManager
{
    public interface IObjectIdentifier
    {
        int FamilyId { get; }
        int ObjectId { get; }
        void RegisterComponents(ObjectLocator objectLocator);
    }
}