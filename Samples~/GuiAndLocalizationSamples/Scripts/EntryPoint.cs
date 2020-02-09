using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow;
using UnityEngine;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private WindowsManager _windowsManager;

        private void Awake()
        {
            _windowsManager = ServiceLocator.Resolve<WindowsManager>();
            _windowsManager.RequestShowWindow(new InitialWindowController());
        }
    }

    public enum WindowType
    {
        Initial, Second, Third
    }
}
