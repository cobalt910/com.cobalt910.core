using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Misc;
using NaughtyAttributes;
using UnityEngine;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    [RequireComponent(typeof(ServiceRegister))]
    public sealed class ServiceLocator : CachedBehaviour
    {
        [ReorderableList]
        public List<ScriptableObject> ScriptableServices = new List<ScriptableObject>();
        private static readonly Dictionary<Type, IService> ServiceMap = new Dictionary<Type, IService>();

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
            if (ServiceMap.ContainsKey(service.ServiceType))
                throw new Exception($"[ServiceLocator] Service {service.ServiceType.Name} already registered.");

            ServiceMap.Add(service.ServiceType, service);
        }

        public static TRegister Resolve<TRegister>() where TRegister : class
        {
            var serviceType = typeof(TRegister);

            if (ServiceMap.ContainsKey(serviceType)) return (TRegister) ServiceMap[serviceType];
            throw new Exception($"[ServiceLocator] Service {serviceType.FullName} was not being register.");
        }

        private void OnDestroy()
        {
            ServiceMap.Clear();
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