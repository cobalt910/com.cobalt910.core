using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Observer
{
    [Serializable]
    public abstract class Observable : IObservable
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        ~Observable()
        {
            var modelName = GetType().FullName;
            foreach (var observerName in _observers.Select(observer => observer.GetType().FullName))
                Debug.LogError(observerName + " not unsubscribed from " + modelName);
        }

        public void SetChanged()
        {
            foreach (var observer in _observers)
                observer.OnObjectChanged(this);
        }

        public void AddObserver(IObserver observable)
        {
            if (_observers.Contains(observable))
                throw new Exception(observable.GetType().FullName + " subscribe duplication.");
            
            _observers.Add(observable);
            observable.OnObjectChanged(this);
        }

        public void RemoveObserver(IObserver observable)
        {
            if (_observers.Contains(observable)) _observers.Remove(observable);
            else throw new Exception(observable.GetType().FullName + " already unsubscribed.");
        }
    }
    
    [Serializable]
    public abstract class Observable<T> : IObservable<T> where T : IObservable<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        ~Observable()
        {
            var modelName = GetType().FullName;
            foreach (var observerName in _observers.Select(observer => observer.GetType().FullName))
                Debug.LogError(observerName + " not unsubscribed from " + modelName);
        }

        public void SetChanged()
        {
            foreach (var observer in _observers)
                observer.OnObjectChanged((T) (object) this);
        }

        public void AddObserver(IObserver<T> observable)
        {
            if (_observers.Contains(observable))
                throw new Exception(observable.GetType().FullName + " subscribe duplication.");
            
            _observers.Add(observable);
            observable.OnObjectChanged((T) (object) this);
        }

        public void RemoveObserver(IObserver<T> observable)
        {
            if (_observers.Contains(observable)) _observers.Remove(observable);
            else throw new Exception(observable.GetType().FullName + " already unsubscribed.");
        }
    }

    public sealed class ReactiveProperty<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                    OnValueChanged?.Invoke(value);
                
                _value = value;
            }
        }
        
        public event Action<T> OnValueChanged;

        public ReactiveProperty(T value)
        {
            _value = value;
        }
    }
}