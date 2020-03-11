using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    public class ServiceInstaller : MonoInstaller
    {
        public bool IncludeInactive = true;
        public SearchStrategy ServiceSearchStrategy = SearchStrategy.InDepthHierarchy;

        public enum SearchStrategy
        {
            WholeObject,
            InDepthHierarchy,
            FirstEntry
        }
        
        public override void InstallBindings()
        {
            #if UNITY_EDITOR
            var obsolete = SearchInterface<IService>(ServiceSearchStrategy);
            obsolete.Where(x => !(x is IMonoService) && !(x is IFactoryService)).ToList().ForEach(x =>
            {
                var errorMsg = $"{x.ServiceType} has not been registered. Obsolete interface, try use {nameof(IMonoService)} or {nameof(IFactoryService)} instead of {nameof(IService)}.";
                Debug.LogError(errorMsg);
            });
            #endif
            
            var services = SearchInterface<IMonoService>(ServiceSearchStrategy);
            services.ForEach(x => Bind(x, Container));
            
            var factories = SearchInterface<IFactoryService>(ServiceSearchStrategy);
            factories.ForEach(x => Bind(x, Container));
        }

        private static void Bind(IService service, DiContainer container)
        {
            container.Bind(service.ServiceType).FromInstance(service).AsCached().NonLazy();
        }

        private List<T> SearchInterface<T>(SearchStrategy searchStrategy)
        {
            switch (searchStrategy)
            {
                case SearchStrategy.WholeObject: return GetComponents<T>().ToList();
                case SearchStrategy.InDepthHierarchy: return GetComponentsInChildren<T>(IncludeInactive).ToList();
                case SearchStrategy.FirstEntry: return new List<T> {GetComponent<T>()};
                default: return new List<T>();
            }
        }
    }
}
