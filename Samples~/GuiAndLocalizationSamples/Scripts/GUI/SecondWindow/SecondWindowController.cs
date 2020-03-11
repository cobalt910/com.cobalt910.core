using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow;
using Zenject;

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

        [Inject] private WindowsManager _windowsManager;
        
        void ISecondWindowController.ClosePressed()
        {
            _windowsManager.RequestShowWindow(new InitialWindowController());
        }
    }
}