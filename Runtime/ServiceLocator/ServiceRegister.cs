using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Misc;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    public sealed class ServiceRegister : CachedBehaviour
    {
        public bool IncludeInactive = true;
        public SearchType ServiceSearchType;
        public enum SearchType
        {
            InDepthHierarchy,
            WholeObject,
            FirstEntry
        }

        private void Awake()
        {
            List<IService> findServices = null;
            switch (ServiceSearchType)
            {
                case SearchType.WholeObject:
                    findServices = new List<IService>(GetComponents<IService>());
                    break;
                case SearchType.InDepthHierarchy:
                    findServices = new List<IService>(GetComponentsInChildren<IService>(IncludeInactive));
                    break;
                case SearchType.FirstEntry:
                    findServices = new List<IService> {GetComponent<IService>()};
                    break;
            }

            findServices?.Where(x => x != null).ToList().ForEach(ServiceLocator.Register);
        }
    }
}