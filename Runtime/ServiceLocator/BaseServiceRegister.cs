using System.Collections.Generic;
using com.cobalt910.core.Runtime.Misc;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    public abstract class BaseServiceRegister : CachedBehaviour
    {
        public abstract List<IService> CollectServices();
    }
}