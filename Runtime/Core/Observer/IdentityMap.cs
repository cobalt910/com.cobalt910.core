using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.Observer
{
    public abstract class IdentityMap<T>
    {
        private readonly Dictionary<string, T> _items = new Dictionary<string, T>();

        public T Add(T item)
        {
            var key = MakeKey(item);

            if (!_items.ContainsKey(key)) _items.Add(key, item);
            else Merge(_items[key], item);

            return _items[key];
        }


        public T Get(string key)
        {
            if (!_items.ContainsKey(key))
                Debug.Log($"Object with key {key} does not exists");

            return _items[key];
        }

        public bool Contains(string key)
        {
            return _items.ContainsKey(key);
        }

        protected abstract string MakeKey(T item);
        protected abstract void Merge(T currentItem, T updatedItem);
    }
}