using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.SecondWindow
{
    public interface ISecondWindowController : IWindowController
    {
        void ClosePressed();
    }

    public class SecondWindowController : ISecondWindowController
    {
        public WindowView View { get; set; }
        public int WindowId { get; } = (int) WindowType.Second;

        private WindowsManager _windowsManager;
        
        public SecondWindowController()
        {
            _windowsManager = ServiceLocator.Resolve<WindowsManager>();
        }

        void ISecondWindowController.ClosePressed()
        {
            _windowsManager.RequestShowWindow(new InitialWindowController());
        }
    }
}