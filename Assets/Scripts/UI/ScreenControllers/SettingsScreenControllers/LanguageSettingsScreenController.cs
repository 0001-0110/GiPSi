using System.Collections.Generic;
using UnityEngine.UI;

public class LanguageSettingsScreenController : ModularScreenController
{
    public LanguageController LanguageController;
    public Dropdown LanguagesSelection;
    public TextController[] textControllers;

    public override void OnEnable()
    {
        base.OnEnable();
        DisplayLanguageSelection();
    }

    /*public override void SetLanguage()
    {
        base.SetLanguage();
        foreach (TextController textController in textControllers)
            textController.UpdateText();
    }*/

    /// <summary>
    /// Display all the avalaible languages, in the current language
    /// </summary>
    private void DisplayLanguageSelection()
    {
        LanguagesSelection.ClearOptions();
        List<string> languages = new List<string>();
        foreach (string language in LanguageController.Languages)
        {
            languages.Add(LanguageController.GetText($"Language_{language}"));
        }
        LanguagesSelection.AddOptions(languages);
        LanguagesSelection.value = LanguageController.Languages.IndexOf(LanguageController.CurrentLanguage);
    }

    public void UpdateLanguage()
    {
        LanguageController.SetLanguage(LanguagesSelection.value);
        SetLanguage();
    }
}
