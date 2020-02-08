using com.cobalt910.core.Runtime.PoolManager;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Extension
{
    public static class PoolManagerExtension
    {
        public static PoolObject InstantiateFromPool(this PoolManager.PoolManager poolManager, GameObject prefab,
            Vector3 position)
        {
            var poolObject = poolManager.InstantiateFromPool(prefab);
            poolObject.Transform.position = position;
            return poolObject;
        }

        public static PoolObject InstantiateFromPool(this PoolManager.PoolManager poolManager, GameObject prefab,
            Vector3 position, Quaternion rotation)
        {
            var poolObject = poolManager.InstantiateFromPool(prefab, position);
            poolObject.Transform.rotation = rotation;
            return poolObject;
        }
    }
}