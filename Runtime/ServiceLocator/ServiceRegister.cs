using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Misc;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    public class ServiceRegister : CachedBehaviour
    {
        public bool IncludeInactive = true;

        public SearchStrategy ServiceSearchStrategy = SearchStrategy.InDepthHierarchy;
        
        public enum SearchStrategy
        {
            WholeObject,
            InDepthHierarchy,
            FirstEntry
        }

        private void Awake()
        {
            List<IService> findServices = null;
            switch (ServiceSearchStrategy)
            {
                case SearchStrategy.WholeObject:
                    findServices = new List<IService>(GetComponents<IService>());
                    break;
                case SearchStrategy.InDepthHierarchy:
                    findServices = new List<IService>(GetComponentsInChildren<IService>(IncludeInactive));
                    break;
                case SearchStrategy.FirstEntry:
                    findServices = new List<IService> {GetComponent<IService>()};
                    break;
            }

            RegisterServices(findServices);
        }

        protected virtual void RegisterServices(List<IService> services)
        {
            services.ForEach(ServiceLocator.Register);
        }
    }
}