using System;
using System.Collections.Generic;
using System.Linq;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using NaughtyAttributes;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Localization
{
    public sealed class LocalizationManager : CachedBehaviour, IService
    {
        Type IService.ServiceType { get; } = typeof(LocalizationManager);
        
        public event Action OnLanguageShanged;
        public string this[string key] => _localizationMap.TryGetValue(key, out var val) ? val : key;
        
        [SerializeField] private string _defaultLanguage = "en";
        [SerializeField] private bool _useTestLanguage;
        [SerializeField, EnableIf("_useTestLanguage")] private string _testLanguage = "ru";
        [SerializeField] private TextAsset[] _localizationJsonAssets;
        
        private readonly Dictionary<string, string> _localizationMap = new Dictionary<string, string>();
        private string _currentLanguage;

        public void SetLanguage(string language)
        {
            _currentLanguage = language;
            UpdateSelectedLanguage();
        }

        public void UpdateSelectedLanguage()
        {
            _localizationMap.Clear();

            var localizationFile = _localizationJsonAssets.First(jsonAsset => jsonAsset.name.Equals(_currentLanguage));
            var localizationJson = localizationFile.ToString();
            var localizationData = JsonUtility.FromJson<LocalizationData>(localizationJson);

            localizationData.LocalizationItems.ToList().ForEach(x => _localizationMap.Add(x.Key, x.Value));

            OnLanguageShanged?.Invoke();
        }
    }
}