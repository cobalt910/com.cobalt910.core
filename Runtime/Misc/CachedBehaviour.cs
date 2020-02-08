using UnityEngine;

namespace com.cobalt910.core.Runtime.Misc
{
    public class CachedBehaviour : MonoBehaviour
    {
        public CachedComponent<Transform> Transform;

        private int _instanceId;
        public int InstanceId
        {
            get
            {
                if (_instanceId == 0) _instanceId = GetInstanceID();
                return _instanceId;
            }
        }

        public CachedBehaviour()
        {
            Transform = new CachedComponent<Transform>(this);
        }
    }

    public sealed class CachedComponent<T> where T : Component
    {
        private T _value;

        public T Value
        {
            get
            {
                if (_value == null) _value = _owner.GetComponent<T>();
                return _value;
            }
        }

        private int _instanceId;

        public int InstanceId
        {
            get
            {
                if (_instanceId == 0) _instanceId = _value.GetInstanceID();
                return _instanceId;
            }
        }

        private readonly Component _owner;

        public CachedComponent(Component owner)
        {
            _owner = owner;
        }
    }
}