using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace com.cobalt910.core.Runtime.PoolManager
{
    public sealed class PoolQueue
    {
        private readonly Queue<PoolObject> _poolObjects = new Queue<PoolObject>();

        private readonly GameObject _prefab;
        private readonly Transform _poolParent;

        private readonly int _increaseSizeBy;
        private bool FlexibleSize => _increaseSizeBy > 0;

        private readonly DiContainer _diContainer;

        public PoolQueue(GameObject prefab, int startSize, int increaseSizeBy, Transform poolRoot, DiContainer diContainer)
        {
            _diContainer = diContainer;

            _prefab = prefab;
            _increaseSizeBy = increaseSizeBy;

            _poolParent = new GameObject($"Pool_{prefab.name}").transform;
            _poolParent.parent = poolRoot;

            for (var i = 0; i < startSize; i++) _poolObjects.Enqueue(CreatePoolObject(_prefab));
        }

        public PoolObject GetPoolObject()
        {
            var poolObject = _poolObjects.Dequeue();
            _poolObjects.Enqueue(poolObject);

            if (!poolObject.IsInsidePool && !FlexibleSize) poolObject.Destroy();
            else if (!poolObject.IsInsidePool && FlexibleSize)
            {
                poolObject = CreatePoolObject(_prefab);
                _poolObjects.Enqueue(poolObject);

                for (var i = 0; i < _increaseSizeBy - 1; i++)
                    _poolObjects.Enqueue(CreatePoolObject(_prefab));
            }

            poolObject.Initialize();

            return poolObject;
        }

        public void DisposePool(float timeToDestroyPool)
        {
            if (_poolObjects.Count == 0) return;
            if (timeToDestroyPool < 0f) throw new ArgumentException($"Argument {nameof(timeToDestroyPool)} should be greater then zero.");

            if (Mathf.Abs(timeToDestroyPool) < 0.0001f)
            {
                while (_poolObjects.Count != 0)
                    Object.Destroy(_poolObjects.Dequeue().GameObject);
            }
            else
            {
                var poolSize = _poolObjects.Count;
                var timePerObject = timeToDestroyPool / poolSize;
                DisposePoolByTime(timePerObject);
            }
        }

        private void DisposePoolByTime(float timePerObject)
        {
            Timer.Timer.Register(timePerObject, () =>
            {
                Object.Destroy(_poolObjects.Dequeue().GameObject);
                DisposePoolByTime(timePerObject);
            });
        }

        private PoolObject CreatePoolObject(GameObject prefab)
        {
            var instance = _diContainer.InstantiatePrefab(prefab, _poolParent);
            instance.SetActive(false);

            return new PoolObject(instance, _poolParent);
        }
    }
}