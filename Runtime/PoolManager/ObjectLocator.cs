using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.cobalt910.core.Runtime.PoolManager
{
    public class ObjectLocator
    {
        private readonly Dictionary<Type, List<object>> _objectsMap = new Dictionary<Type, List<object>>();

        public void Register<T>(T objectToSave) where T : class
        {
            var saveType = typeof(T);
            
            if (_objectsMap.TryGetValue(saveType, out var components)) components.Add(objectToSave);
            else _objectsMap.Add(saveType, new List<object> {objectToSave});
        }

        public TSave Resolve<TSave>() where TSave : Component
        {
            var saveType = typeof(TSave);

            if (_objectsMap.TryGetValue(saveType, out var objects))
                return (TSave) objects[0];

            return null;
        }

        public bool TryResolve<TSave>(out TSave instance) where TSave : class
        {
            var saveType = typeof(TSave);

            if (_objectsMap.TryGetValue(saveType, out var components))
            {
                instance = (TSave) components[0];
                return true;
            }

            instance = null;
            return false;
        }

        public bool TryResolveMany<TSave>(out List<TSave> instances) 
        {
            var saveType = typeof(TSave);

            if (_objectsMap.TryGetValue(saveType, out var objects))
            {
                instances = objects.Select(x => (TSave) x).ToList();
                return true;
            }

            instances = null;
            return false;
        }
    }
}