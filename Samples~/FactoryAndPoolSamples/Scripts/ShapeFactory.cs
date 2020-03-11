using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Factory;
using com.cobalt910.core.Runtime.PoolManager;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Samples.FactoryAndPoolSamples.Scripts
{
    public class ShapeFactory : MonoFactory<ShapeCreationArgs, Shape>
    {
        public override Type ServiceType { get; } = typeof(MonoFactory<ShapeCreationArgs, Shape>);

        [SerializeField] private List<Shape> _shapePrefabs = new List<Shape>();
        private Dictionary<ShapeType, GameObject> _shapePrefabsMap;

        [Inject] private PoolManager _poolManager;
        
        private void Awake()
        {
            _shapePrefabs.ForEach(x => _poolManager.CreatePool(x.gameObject, 5, 1));
            _shapePrefabsMap = _shapePrefabs.ToDictionary(x => x.ShapeType, x => x.gameObject);
        }

        public override Shape Create(ShapeCreationArgs args)
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