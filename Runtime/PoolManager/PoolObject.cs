using System;
using UnityEngine;

namespace com.cobalt910.core.Runtime.PoolManager
{
    [Serializable]
    public sealed class PoolObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public Transform PoolParent { get; }

        public int InstanceId { get; }
        public bool IsInsidePool { get; private set; }
        public ObjectLocator ObjectLocator { get; }

        public event Action OnDestroyed;

        private IPoolObject[] _poolObjectScripts;

        public PoolObject(GameObject instance, Transform poolParent)
        {
            GameObject = instance;
            InstanceId = instance.GetInstanceID();
            Transform = instance.transform;
            _poolObjectScripts = instance.GetComponentsInChildren<IPoolObject>(true);

            PoolParent = poolParent;
            ObjectLocator = new ObjectLocator();
            
            foreach (var poolObjectScript in _poolObjectScripts)
                poolObjectScript.PostAwake(this);
        }
        
        public void Initialize()
        {
            GameObject.SetActive(true);
            IsInsidePool = false;
            
            foreach (var iPoolObject in _poolObjectScripts)
                iPoolObject.OnReuseObject(this);
        }

        /// <summary>
        /// Return object back into pool.
        /// </summary>
        public void Destroy() // todo: not safety, should cleanup fields and save copy of object
        {
            GameObject.SetActive(false);
            Transform.position = Vector3.zero;
            Transform.rotation = Quaternion.identity;
            Transform.parent = PoolParent;
            IsInsidePool = true;

            foreach (var poolObjectScript in _poolObjectScripts)
                poolObjectScript.OnDisposeObject(this);
            
            OnDestroyed?.Invoke();
            OnDestroyed = delegate { };
        }
    }
}