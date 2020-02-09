using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.cobalt910.core.Runtime.PoolManager
{
    public class ObjectLocator
    {
        private readonly Dictionary<Type, List<object>> _objectsMap = new Dictionary<Type, List<object>>();

        public void Register<TSave>(TSave objectToSave) where TSave : class
        {
            var saveType = typeof(TSave);
            
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

        public bool TryResolveMany<TSave>(out List<TSave> instances)  where TSave : class
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

        public void Forget<TSave>() where TSave : class
        {
            var saveType = typeof(TSave);

            if (_objectsMap.ContainsKey(saveType))
            {
                var list = _objectsMap[saveType];
                if (list.Count > 0) list.RemoveAt(0);
            }
        }

        public void ForgetAll<TSave>() where TSave : class
        {
            var saveType = typeof(TSave);
            if (_objectsMap.ContainsKey(saveType))
                _objectsMap.Remove(saveType);
        }

        public void Clear()
        {
            _objectsMap.Clear();
        }
    }
}