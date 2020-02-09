using com.cobalt910.core.Runtime.GUI;
using UnityEngine;
using UnityEngine.UI;

namespace com.cobalt910.core.Samples.GuiAndLocalizationSamples.Scripts.GUI.SecondWindow
{
    public class SecondWindowView : BaseWindowView<ISecondWindowController>
    {
        public override int WindowId { get; }

        [SerializeField] private Button _closeButton;
        
        
        protected override void Initialize()
        {
            _closeButton.onClick.AddListener(OnCloseButtonPressed);
        }

        protected override void Dispose()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }

        private void OnCloseButtonPressed() => ConcreteController.ClosePressed();
    }
}