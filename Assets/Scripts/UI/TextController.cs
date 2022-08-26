using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : MonoBehaviour
{
    private LanguageController languageController;
    private Text text;

    public string LocalizationString;
    // TODO add the option to give this text controller a formated localization string
    //public string FormatLocalizationString;
    //public string[] LocalizationStrings;

    void Awake()
    {
        languageController = LanguageController.Instance;
        languageController.TextControllers.Add(this);

        text = GetComponent<Text>();

        // Only needed once, because LanguageController will update this text again when needed
        UpdateText();
    }

    public void UpdateText()
    {
        if (LocalizationString == string.Empty)
        {
            Debug.LogWarning("WARNING: Language - Missing localization string", gameObject);
            return;
        }
        SetText(LocalizationString);
    }

    public void SetText(string localizationString)
    {
        LocalizationString = localizationString;
        // text might be null if SetText is called before this object is enabled
        // This is not a problem tho, as it will only update the localization string and wait for OnEnable to update the text
        if (text != null)
        {
            text.text = languageController.GetText(localizationString);
        }
    }
}
