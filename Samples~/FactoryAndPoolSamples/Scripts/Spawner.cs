using System;
using com.cobalt910.core.Runtime.Factory;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.cobalt910.core.Samples.FactoryAndPoolSamples.Scripts
{
    public class Spawner : CachedBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _pushForce;
        [SerializeField] private float _rotationForce;

        private IFactory<Shape, ShapeCreationArgs> _shapeFactory;
        
        private float _timer;

        private void Awake()
        {
            _shapeFactory = ServiceLocator.Resolve<IFactory<Shape, ShapeCreationArgs>>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnDelay)
            {
                _timer = 0f;

                var shapeType = GetRandomShape();
                var position = GetPosOnCircle(_spawnRadius);
                var shape = SpawnShape(shapeType, position);
                
                shape.Rigidbody.AddForce(-(Vector3.down * 10 - position).normalized * _pushForce);
                shape.Rigidbody.AddTorque(Random.insideUnitSphere * _rotationForce);
            }
        }

        private Shape SpawnShape(ShapeType shapeType, Vector3 position)
        {
            return _shapeFactory.Create(new ShapeCreationArgs(shapeType, position));
        }

        private ShapeType GetRandomShape()
        {
            var shapesCount = Enum.GetValues(typeof(ShapeType)).Length;
            return (ShapeType) Random.Range(0, shapesCount);
        }

        private Vector3 GetPosOnCircle(float circleRadius)
        {
            var spherePosition = Random.insideUnitSphere.normalized * circleRadius;
            spherePosition.y = 0f;
            return spherePosition;
        }
    }
}
