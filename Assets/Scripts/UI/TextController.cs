using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be placed as a componnent of the text object
/// </summary>
public class TextController : MonoBehaviour
{
    private LanguageController languageController;
    private Text text;

    public string localizationString;

    void Awake()
    {
        languageController = GameObject.Find("LanguageController").GetComponent<LanguageController>();
        text = GetComponent<Text>();
    }

    // Not the most efficient for sure, but it's not that bad either
    // So for now, it'll do
    void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        SetText(localizationString);
    }

    public void SetText(string localizationString)
    {
        this.localizationString = localizationString;
        // text might be null if SetText is called before this object is enabled
        // This is not a problem tho, as it will only update the localization string and wiat for OnEnable to update the text
        if (text != null)
            text.text = languageController.GetText(localizationString);
    }
}
