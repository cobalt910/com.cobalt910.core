using System;
using System.Collections.Generic;
using System.Net;
using ProjectCore.Localization;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace ProjectCore.GUI
{
    public sealed class WindowsManager : CachedBehaviour, IService
    {
        Type IService.ServiceType { get; } = typeof(WindowsManager);
        public IWindowController OpenedWindow { get; private set; }

        [SerializeField] private WindowView[] _views;
        
        private readonly Dictionary<int, WindowView> _windowsMap = new Dictionary<int, WindowView>();
        private LocalizationManager _localizationManager;

        private void Awake()
        {
            _localizationManager = ServiceLocator.ServiceLocator.Resolve<LocalizationManager>();
            
            foreach (var view in _views)
            {
                _windowsMap.Add(view.WindowId, view);
                view.gameObject.SetActive(false);
            }

            _views = null;

            _localizationManager.OnLanguageShanged += LanguageChanged;
        }

        public void RequestShowPopup(IWindowController controller)
        {
            if (OpenedWindow != null && !OpenedWindow.View.IsOpened)
            {
                var currentPopup = OpenedWindow;
                currentPopup.View.Close();
                currentPopup.View = null;
            }

            _windowsMap.TryGetValue(controller.WindowId, out var view);
            if (view.IsNull()) throw new NullReferenceException("Can't find popup with id: " + controller.WindowId);
            
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
    }
}