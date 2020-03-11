using System;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Runtime.SoundManager
{
    public class SoundManager : CachedBehaviour, IMonoService
    {
        Type IService.ServiceType { get; } = typeof(SoundManager);
        [SerializeField] private SoundPlayer _soundPlayerPrefab;
        
        private const int InitialPoolSize = 20;
        private const int IncreasePoolBy = 20;

        [Inject] private PoolManager.PoolManager _poolManager;

        private void Awake()
        {
            _poolManager.CreatePool(_soundPlayerPrefab.gameObject, InitialPoolSize, IncreasePoolBy);
        }

        public SoundPlayer FromFactory()
        {
            var poolObject = _poolManager.InstantiateFromPool(_soundPlayerPrefab.gameObject);
            return poolObject.ObjectLocator.Resolve<SoundPlayer>();
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
