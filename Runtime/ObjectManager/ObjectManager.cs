using System;
using System.Collections.Generic;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using UnityEngine;

namespace com.cobalt910.core.Runtime.ObjectManager
{
    public abstract class ObjectManager<TContract> : CachedBehaviour, IService where TContract : IObjectIdentifier
    {
        public Type ServiceType => GetType();
        private readonly Dictionary<int, TContract> _objectsMap = new Dictionary<int, TContract>();

        public void Register(TContract objectToRegister)
        {
            _objectsMap.Add(objectToRegister.ObjectId, objectToRegister);
        }

        public TContract Resolve(RaycastHit raycastHit)
        {
            return _objectsMap[raycastHit.collider.GetInstanceID()];
        }

        public void Remove(TContract objectToRemove)
        {
            _objectsMap.Remove(objectToRemove.ObjectId);
        }
    }
}