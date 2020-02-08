using System;
using TMPro;

namespace com.cobalt910.core.Runtime.Localization
{
    [Serializable]
    public sealed class LocalizationData
    {
        public LocalizationItem[] LocalizationItems;
    }
    
    [Serializable]
    public sealed class LocalizationItem
    {
        public string Key;
        public string Value;
    }

    [Serializable]
    public sealed class TextItem
    {
        public string LocalizationKey;
        public bool Ignore;
        public TMP_Text TextComponent;

        public void UpdateText(LocalizationManager localizationManager)
        {
            if(Ignore) return;
            TextComponent.text = localizationManager[LocalizationKey];
        }
    }
}