using com.cobalt910.core.Runtime.GUI;
using UnityEngine;
using UnityEngine.UI;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.InitialWindow
{
    public class InitialWindowView : BaseWindowView<IInitialWindowController>
    {
        public override int WindowId => (int) WindowType.Initial;

        [SerializeField] private Button _openSecondWindowButton;
        
        
        protected override void Initialize()
        {
            _openSecondWindowButton.onClick.AddListener(OnSecondButtonClicked);
        }

        protected override void Dispose()
        {
            _openSecondWindowButton.onClick.RemoveListener(OnSecondButtonClicked);
        }

        private void OnSecondButtonClicked() => ConcreteController.SecondButtonClicked();
    }
}