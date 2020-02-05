using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectCore.PoolManager
{
    public class LocalComponentLocator
    {
        private readonly Dictionary<Type, List<Component>> _componentsMap = new Dictionary<Type, List<Component>>();

        public void Register(Type saveType, Component component)
        {
            if (_componentsMap.TryGetValue(saveType, out var components)) components.Add(component);
            else _componentsMap.Add(saveType, new List<Component> {component});
        }

        public TSave Resolve<TSave>() where TSave : Component
        {
            var saveType = typeof(TSave);

            if (_componentsMap.TryGetValue(saveType, out var components))
                return (TSave) components[0];

            return null;
        }

        public bool TryResolve<TSave>(out TSave instance) where TSave : Component
        {
            var saveType = typeof(TSave);

            if (_componentsMap.TryGetValue(saveType, out var components))
            {
                instance = (TSave) components[0];
                return true;
            }

            instance = null;
            return false;
        }

        public bool TryResolveMany<TSave>(out List<TSave> instance) where TSave : Component
        {
            var saveType = typeof(TSave);

            if (_componentsMap.TryGetValue(saveType, out var components))
            {
                instance = components.Select(x => (TSave) x).ToList();
                return true;
            }

            instance = null;
            return false;
        }
    }
}