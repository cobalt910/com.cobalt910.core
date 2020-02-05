using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.cobalt910.core.Runtime.Core.PoolManager
{
    public sealed class PoolQueue
    {
        private readonly Queue<PoolObject> _poolObjects = new Queue<PoolObject>();

        private readonly GameObject _prefab;
        private readonly Transform _poolParent;

        private readonly int _increaseSizeBy;
        private bool FlexibleSize => _increaseSizeBy > 0;

        public PoolQueue(GameObject prefab, int startSize, int increaseSizeBy, Transform poolRoot)
        {
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

            if (poolObject.GameObject.activeSelf && !FlexibleSize) poolObject.Destroy();
            else if (poolObject.GameObject.activeSelf && FlexibleSize)
            {
                poolObject = CreatePoolObject(_prefab);
                _poolObjects.Enqueue(poolObject);

                for (int i = 0; i < _increaseSizeBy - 1; i++)
                    _poolObjects.Enqueue(CreatePoolObject(_prefab));
            }

            poolObject.Initialize();

            return poolObject;
        }

        public void DisposePool(float timeToDestroyPool)
        {
            if (_poolObjects.Count == 0) return;

            if (Math.Abs(timeToDestroyPool) < 0.0001f)
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
            var instance = Object.Instantiate(prefab, _poolParent);
            instance.SetActive(false);

            return new PoolObject(instance, _poolParent);
        }
    }
}