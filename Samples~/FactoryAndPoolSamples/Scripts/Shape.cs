using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.PoolManager;
using com.cobalt910.core.Runtime.Timer;
using UnityEngine;

namespace com.cobalt910.core.Samples.FactoryAndPoolSamples.Scripts
{
    public class Shape : CachedBehaviour, IPoolObject
    {
        public ShapeType ShapeType;
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        #region IPoolObject implementation
        void IPoolObject.PostAwake(PoolObject poolObject)
        {
            poolObject.ObjectLocator.Register(typeof(Shape), this);
        }

        void IPoolObject.OnReuseObject(PoolObject poolObject)
        {
            Timer.Register(3f, poolObject.Destroy);
        }

        void IPoolObject.OnDisposeObject(PoolObject poolObject)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        #endregion
        
    }
    
    public enum ShapeType
    {
        Cube, Sphere, Cylinder
    }
}