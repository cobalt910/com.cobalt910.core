using System;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Core.PoolManager
{
    [Serializable]
    public sealed class PoolObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public Transform Parent { get; }

        public int InstanceId { get; }
        public IPoolObject[] PoolObjectScripts { get; }
        public bool IsInsidePool { get; private set; }
        public ObjectLocator objectLocator { get; }

        public event Action OnDestroyed;

        public PoolObject(GameObject instance, Transform parent)
        {
            GameObject = instance;
            InstanceId = instance.GetInstanceID();
            Transform = instance.transform;
            PoolObjectScripts = instance.GetComponentsInChildren<IPoolObject>(true);

            Parent = parent;

            foreach (var poolObjectScript in PoolObjectScripts)
                poolObjectScript.PostAwake(this);
            
            objectLocator = new ObjectLocator();
        }
        
        public void Initialize()
        {
            GameObject.SetActive(true);
            IsInsidePool = false;
            
            foreach (var iPoolObject in PoolObjectScripts)
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
            Transform.parent = Parent;
            IsInsidePool = true;

            foreach (var poolObjectScript in PoolObjectScripts)
                poolObjectScript.OnDisposeObject(this);
            
            OnDestroyed?.Invoke();
            OnDestroyed = delegate { };
        }
    }
}