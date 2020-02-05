using ProjectCore.PoolManager;
using UnityEngine;

namespace ProjectCore.Misc
{
    public static class PoolManagerUtils
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