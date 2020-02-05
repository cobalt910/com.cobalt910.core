using System;
using com.cobalt910.core.Runtime.Core.Misc;
using com.cobalt910.core.Runtime.Core.ServiceLocator;
using UnityEditor;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Core.SoundManager
{
    public class SoundManager : CachedBehaviour, IService
    {
        Type IService.ServiceType { get; } = typeof(SoundManager);
        [SerializeField] private SoundPlayer _soundPlayerPrefab;
        
        private const int InitialPoolSize = 20;
        private const int IncreasePoolBy = 20;

        private PoolManager.PoolManager _poolManager;
        
        private void Awake()
        {
            _poolManager = ServiceLocator.ServiceLocator.Resolve<PoolManager.PoolManager>();
            _poolManager.CreatePool(_soundPlayerPrefab.gameObject, InitialPoolSize, IncreasePoolBy);
        }

        public SoundPlayer FromFactory()
        {
            var poolObject = _poolManager.InstantiateFromPool(_soundPlayerPrefab.gameObject);
            return poolObject.objectLocator.Resolve<SoundPlayer>();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_soundPlayerPrefab != null) return;
            _soundPlayerPrefab = Resources.Load<SoundPlayer>(nameof(SoundPlayer));
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
