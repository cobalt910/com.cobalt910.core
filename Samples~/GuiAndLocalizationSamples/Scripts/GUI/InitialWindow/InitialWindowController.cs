using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.SecondWindow;
using Zenject;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow
{
    public interface IInitialWindowController : IWindowController
    {
        void SecondButtonClicked();
    }

    public class InitialWindowController : IInitialWindowController
    {
        public WindowView View { get; set; }
        public int WindowId => (int) WindowType.Initial;

        [Inject] private WindowsManager _windowsManager;

        void IInitialWindowController.SecondButtonClicked()
        {
            _windowsManager.RequestShowWindow(new SecondWindowController());
        }
    }
}