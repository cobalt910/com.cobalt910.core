using com.cobalt910.core.Runtime.GUI;
using com.cobalt910.core.Runtime.ServiceLocator;
using com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow;
using UnityEngine;
using Zenject;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private WindowsManager _windowsManager;

        private void Awake()
        {
            _windowsManager.RequestShowWindow(new InitialWindowController());
        }
    }

    public enum WindowType
    {
        Initial, Second, Third
    }
}
