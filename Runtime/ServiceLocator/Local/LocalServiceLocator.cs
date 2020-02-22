using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Misc;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace com.cobalt910.core.Runtime.ServiceLocator.Local
{
    [RequireComponent(typeof(LocalServiceRegisterer))]
    public class LocalServiceLocator : CachedBehaviour
    {
        [ReorderableList]
        public List<ScriptableObject> ScriptableServices = new List<ScriptableObject>();
        private readonly Dictionary<Type, IService> _serviceMap = new Dictionary<Type, IService>();
        
        public int InstanceId { get; private set; }
        private List<GameObject> _allChildGameObjects = new List<GameObject>();
        private Dictionary<int, GameObject> _childsMap;
        
        private void Awake()
        {
            _childsMap = _allChildGameObjects.ToDictionary(x => x.GetInstanceID());

            InstanceId = GetInstanceID();
            ServiceLocator.AddLocalServiceLocator(this);
        }
        
        public void Register(IService service)
        {
            if (_serviceMap.ContainsKey(service.ServiceType))
                throw new Exception($"[LocalServiceLocator] Service {service.ServiceType.Name} already registered.");

            _serviceMap.Add(service.ServiceType, service);
        }

        public TRegister Resolve<TRegister>() where TRegister : class
        {
            var serviceType = typeof(TRegister);

            if (_serviceMap.ContainsKey(serviceType)) return (TRegister) _serviceMap[serviceType];
            throw new Exception($"[LocalServiceLocator] Service {serviceType.FullName} was not being register.");
        }
        
        public bool Contains(GameObject sendRequestFrom)
        {
            return _childsMap.ContainsKey(sendRequestFrom.GetInstanceID());
        }

        private void OnDestroy()
        {
            ServiceLocator.RemoveLocalServiceLocator(this);
        }

#if UNITY_EDITOR
        [ShowNativeProperty]
        private int _bakedChilds => _allChildGameObjects.Count;
        [Button("Re-Bake Child Id's")] 
        private void ReBakeChildIds()
        {
            _allChildGameObjects = GetComponentsInChildren<Transform>().Select(x => x.gameObject).ToList();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}