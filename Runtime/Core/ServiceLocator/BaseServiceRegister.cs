using System.Collections.Generic;
using ProjectCore.Misc;

namespace ProjectCore.ServiceLocator
{
    public abstract class BaseServiceRegister : CachedBehaviour
    {
        public abstract List<IService> CollectServices();
    }
}