using System;
using System.Collections.Generic;
using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Factory;
using com.cobalt910.core.Runtime.PoolManager;
using com.cobalt910.core.Runtime.ServiceLocator;
using UnityEngine;

namespace com.cobalt910.core.Samples.FactoryAndPoolSamples.Scripts
{
    public class ShapeFactory : MonoBehaviour, IFactory<Shape, ShapeCreationArgs>
    {
        Type IService.ServiceType { get; } = typeof(IFactory<Shape, ShapeCreationArgs>);

        [SerializeField] private List<Shape> _shapePrefabs = new List<Shape>();
        private readonly Dictionary<ShapeType, GameObject> _shapePrefabsMap = new Dictionary<ShapeType, GameObject>();

        private PoolManager _poolManager;
        
        private void Awake()
        {
            _poolManager = ServiceLocator.Resolve<PoolManager>();
            
            _shapePrefabs.ForEach(x =>
            {
                _shapePrefabsMap.Add(x.ShapeType, x.gameObject);
                _poolManager.CreatePool(x.gameObject, 10);
            });
        }

        public Shape Create(ShapeCreationArgs args)
        {
            var poolObject = _poolManager.InstantiateFromPool(_shapePrefabsMap[args.ShapeType], args.Position);
            return poolObject.ObjectLocator.Resolve<Shape>();
        }
    }
    
    public class ShapeCreationArgs
    {
        public readonly ShapeType ShapeType;
        public readonly Vector3 Position;

        public ShapeCreationArgs(ShapeType shapeType, Vector3 position)
        {
            ShapeType = shapeType;
            Position = position;
        }
    }
}