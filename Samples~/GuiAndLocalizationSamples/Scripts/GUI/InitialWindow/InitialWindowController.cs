using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.SecondWindow;

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

        private WindowsManager _windowsManager;
        
        public InitialWindowController()
        {
            _windowsManager = ServiceLocator.Resolve<WindowsManager>();
        }

        void IInitialWindowController.SecondButtonClicked()
        {
            _windowsManager.RequestShowWindow(new SecondWindowController());
        }
    }
}