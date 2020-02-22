using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator.Local;
using NaughtyAttributes;
using UnityEngine;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    [RequireComponent(typeof(ServiceRegister))]
    public sealed class ServiceLocator : CachedBehaviour
    {
        [ReorderableList]
        public List<ScriptableObject> ScriptableServices = new List<ScriptableObject>();
       
        private static readonly Dictionary<Type, IService> _serviceMap = new Dictionary<Type, IService>();
        private static readonly Dictionary<int, LocalServiceLocator> _localServiceLocators = new Dictionary<int, LocalServiceLocator>();
        
        private void Awake()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ScriptableServices.ForEach(x => Register((IService) x));
            
            GetComponentsInChildren<BaseServiceRegister>()
                .SelectMany(x => x.CollectServices())
                .ToList()
                .ForEach(Register);
        }

        public static void Register(IService service)
        {
            if (_serviceMap.ContainsKey(service.ServiceType))
                throw new Exception($"[ServiceLocator] Service {service.ServiceType.Name} already registered.");

            _serviceMap.Add(service.ServiceType, service);
        }

        public static TRegister Resolve<TRegister>(GameObject localContext = null) where TRegister : class
        {
            if (localContext.IsNotNull()) return ResolveLocal<TRegister>(localContext);
            
            var serviceType = typeof(TRegister);

            if (_serviceMap.ContainsKey(serviceType)) return (TRegister) _serviceMap[serviceType];
            throw new Exception($"[ServiceLocator] Service {serviceType.FullName} was not being register.");
        }

        private static TRegister ResolveLocal<TRegister>(GameObject sendRequestFrom) where TRegister : class
        {
            if(sendRequestFrom.IsNull())
                throw new ArgumentNullException(nameof(sendRequestFrom), "Parameter should not be equal to null.");
            
            var concreteLocator = _localServiceLocators.Values.FirstOrDefault(localServiceLocator => localServiceLocator.Contains(sendRequestFrom));
            if (concreteLocator.IsNotNull()) return concreteLocator.Resolve<TRegister>();
            
            throw new Exception("[ServiceLocator] Local service locator with requested parameters not found.");
        }

        private void OnDestroy()
        {
            _serviceMap.Clear();
        }

        internal static void AddLocalServiceLocator(LocalServiceLocator localServiceLocator)
        {
            // todo: checks and debugs
            _localServiceLocators.Add(localServiceLocator.InstanceId, localServiceLocator);
        }

        internal static void RemoveLocalServiceLocator(LocalServiceLocator localServiceLocator)
        {
            // todo: checks and debugs
            _localServiceLocators.Remove(localServiceLocator.InstanceId);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (var i = 0; i < ScriptableServices.Count; i++)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                if (!(ScriptableServices[i] is IService))
                {
                    Debug.LogError($"ScriptableObject: {ScriptableServices[i].name} was not implement IService.");
                    ScriptableServices.RemoveAt(i);
                    i--;
                }
            }
        }
#endif
    }
}