using System;
using System.Collections.Generic;
using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Localization;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Runtime.GUI
{
    public sealed class WindowsManager : CachedBehaviour, IMonoService
    {
        Type IService.ServiceType { get; } = typeof(WindowsManager);
        public IWindowController OpenedWindow { get; private set; }

        [SerializeField, ReorderableList] private WindowView[] _windows;
        
        private readonly Dictionary<int, WindowView> _windowsMap = new Dictionary<int, WindowView>();
        [Inject] private LocalizationManager _localizationManager;

        private void Awake()
        {
            foreach (var view in _windows)
            {
                _windowsMap.Add(view.WindowId, view);
                view.gameObject.SetActive(false);
            }

            _windows = null;

            _localizationManager.OnLanguageShanged += LanguageChanged;
        }

        public void RequestShowWindow(IWindowController controller)
        {
            if (OpenedWindow != null && OpenedWindow.View.IsOpened)
            {
                var currentPopup = OpenedWindow;
                currentPopup.View.Close();
                currentPopup.View = null;
            }

            _windowsMap.TryGetValue(controller.WindowId, out var view);
            if (view.IsNull()) throw new NullReferenceException("Can't find popup with id: " + controller.WindowId);
            
            // ReSharper disable once PossibleNullReferenceException
            view.Controller = controller;
            controller.View = view;

            OpenedWindow = controller;
            view.Transform.Value.SetAsLastSibling();

            view.UpdateLanguage(_localizationManager);
            view.Open();
        }

        private void LanguageChanged()
        {
            if (OpenedWindow != null && OpenedWindow.View != null && OpenedWindow.View.IsOpened)
                OpenedWindow.View.UpdateLanguage(_localizationManager);
        }
        
#if UNITY_EDITOR
        [Button("Collect All Windows")]
        private void CollectAllWindows()
        {
            _windows = GetComponentsInChildren<WindowView>(true);
            EditorUtility.SetDirty(this);
        }
#endif
    }
}