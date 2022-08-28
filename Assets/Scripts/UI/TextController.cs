using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : MonoBehaviour
{
    // TODO better name needed
    // Regex that matches every localization in between curly braces
    private static Regex regex = new Regex("{([^}]*)}");

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
        
        SetText(LocalizationString);
    }

    public void SetText(string localizationString)
    {
        if (localizationString == string.Empty)
        {
            Debug.LogWarning("WARNING: Language - Invalid localization string", gameObject);
            return;
        }

        LocalizationString = localizationString;
        // text might be null if SetText is called before this object is enabled
        // This is not a problem tho, as it will only update the localization string and wait for OnEnable to update the text
        if (text != null)
        {
            text.text = localizationString;
            Match match = regex.Match(localizationString);
            for (int i = 1; i < match.Groups.Count; i++)
                text.text = regex.Replace(text.text, languageController.GetText(match.Groups[i].Value));
        }
    }
}
