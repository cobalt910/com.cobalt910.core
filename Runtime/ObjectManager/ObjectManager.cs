using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.PoolManager;
using com.cobalt910.core.Runtime.ServiceLocator;
using UnityEngine;

namespace com.cobalt910.core.Runtime.ObjectManager
{
    public class ObjectManager : MonoBehaviour, IService
    {
        public Type ServiceType { get; } = typeof(ObjectManager);
        
        // objectId -- ObjectData
        private readonly Dictionary<int, ObjectData> _objectsMap = new Dictionary<int, ObjectData>();
        
        public void Register(IObjectIdentifier objectIdentifier)
        {
            if(objectIdentifier == null)
                throw new NullReferenceException($"Parameter {nameof(objectIdentifier)} cannot be null.");
            
            if(_objectsMap.ContainsKey(objectIdentifier.ObjectId))
                throw new Exception("Object already registered.");
            
            _objectsMap.Add(objectIdentifier.ObjectId, new ObjectData(objectIdentifier));
        }

        public void Remove(IObjectIdentifier objectIdentifier)
        {
            if(objectIdentifier == null)
                throw new NullReferenceException($"Parameter {nameof(objectIdentifier)} cannot be null.");
            
            if(!_objectsMap.ContainsKey(objectIdentifier.ObjectId))
                throw new Exception("Object was not being registered.");

            _objectsMap.Remove(objectIdentifier.ObjectId);
        }

        public ObjectLocator Resolve(int objectId)
        {
            _objectsMap.TryGetValue(objectId, out var objectData);
            if(objectData == null) throw new NullReferenceException("Object was not being registered.");

            return objectData.ObjectLocator;
        }

        public int GetFamily(int objectId)
        {
            _objectsMap.TryGetValue(objectId, out var objectData);
            if(objectData == null) throw new NullReferenceException("Object was not being registered.");

            return objectData.ObjectIdentifier.FamilyId;
        }

        public void GetAllObjectsFromFamily(int familyId, out List<ObjectLocator> components)
        {
            components = _objectsMap
                .Where(x => x.Value.ObjectIdentifier.FamilyId == familyId)
                .Select(x => x.Value.ObjectLocator)
                .ToList();
        }

        private class ObjectData
        {
            public readonly ObjectLocator ObjectLocator;
            public readonly IObjectIdentifier ObjectIdentifier;

            public ObjectData(IObjectIdentifier objectIdentifier)
            {
                ObjectIdentifier = objectIdentifier;
                ObjectLocator = new ObjectLocator();
            
                ObjectIdentifier.RegisterComponents(ObjectLocator);
            }
        }
    }
}
