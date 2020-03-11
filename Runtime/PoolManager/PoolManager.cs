using System;
using System.Collections.Generic;
using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using TMPro;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Runtime.PoolManager
{
    public sealed class PoolManager : CachedBehaviour, IMonoService
    {
        Type IService.ServiceType => typeof(PoolManager);
        
        private readonly Dictionary<int, PoolQueue> _poolMap = new Dictionary<int, PoolQueue>();
        [Inject] private DiContainer _diContainer;

        /// <summary>
        /// Create pool using certain prefab.
        /// </summary>
        /// <param name="prefab">Pool prefab.</param>
        /// <param name="startSize">Initial pool size.</param>
        /// <param name="increaseSizeBy">Increase size if pool was ended on amount.</param>
        /// <exception cref="Exception">thrown exception if pool already exist.</exception>
        public void CreatePool(GameObject prefab, int startSize, int increaseSizeBy = 0)
        {
            if (prefab.IsNull()) throw new ArgumentException($"Parameter {nameof(prefab)} cannot be null.");
            if (startSize <= 0) throw new ArgumentException($"Parameter {nameof(startSize)} should be greater then zero.");
            if (increaseSizeBy < 0) throw new ArgumentException($"Parameter {nameof(increaseSizeBy)} cannot be less then zero.");
            
            var poolKey = prefab.GetInstanceID();

            if (_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} already exist.");

            var pool = new PoolQueue(prefab, startSize, increaseSizeBy, Transform.Value, _diContainer);
            _poolMap.Add(poolKey, pool);
        }

        /// <summary>
        /// Grabbing object from pool.
        /// </summary>
        /// <param name="prefab">Prefab was being used for creating pool.</param>
        /// <returns>Return container for pool object</returns>
        /// <exception cref="Exception">thrown exception if pool not exist.</exception>
        public PoolObject InstantiateFromPool(GameObject prefab)
        {
            if (prefab.IsNull()) throw new ArgumentException($"Parameter {nameof(prefab)} cannot be null.");

            var poolKey = prefab.GetInstanceID();

            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            return _poolMap[poolKey].GetPoolObject();
        }

        public void DisposePool(GameObject prefab, float timeToDestroyPool = 0)
        {
            if (timeToDestroyPool < 0) throw new ArgumentException($"Parameter {timeToDestroyPool} cannot be less then zero.");
            
            var poolKey = prefab.GetInstanceID();

            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            _poolMap[poolKey].DisposePool(timeToDestroyPool);
            _poolMap.Remove(poolKey);
        }
    }
}