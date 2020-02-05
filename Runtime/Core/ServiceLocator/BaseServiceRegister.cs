using System.Collections.Generic;
using com.cobalt910.core.Runtime.Core.Misc;

namespace com.cobalt910.core.Runtime.Core.ServiceLocator
{
    public abstract class BaseServiceRegister : CachedBehaviour
    {
        public abstract List<IService> CollectServices();
    }
}