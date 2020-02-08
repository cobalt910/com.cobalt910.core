using System;
using com.cobalt910.core.Runtime.Localization;
using com.cobalt910.core.Runtime.Misc;

namespace com.cobalt910.core.Runtime.GUI
{
    public abstract class WindowView : CachedBehaviour 
    {
        public TextItem[] LocalizationText;
        public abstract int WindowId { get; }
        public virtual IWindowController Controller { get; set; }
        public virtual bool IsOpened { get; protected set; }

        public event Action<IWindowController> OnPopupClosed;

        public virtual void Open()
        {
            gameObject.SetActive(true);
            Initialize();
            IsOpened = true;
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            Dispose();
            IsOpened = false;

            OnPopupClosed?.Invoke(Controller);
            Controller = null;
        }

        public virtual void UpdateLanguage(LocalizationManager localizationManager)
        {
            foreach (var textItem in LocalizationText)
                textItem.UpdateText(localizationManager);
        }

        protected abstract void Initialize();
        protected abstract void Dispose();
    }

    public abstract class BaseWindowView<T> : WindowView where T : class, IWindowController
    {
        public T ConcreteController => (T) Controller;
    }
}
