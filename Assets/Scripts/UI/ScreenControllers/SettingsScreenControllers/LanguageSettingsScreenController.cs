using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

using Services;

public class LanguageSettingsScreenController : ModularScreenController
{
    public LanguageController LanguageController;
    public Dropdown LanguagesSelection;
    public TextController[] textControllers;

    private Dictionary<Language, string> languages = new Dictionary<Language, string>()
    {
        [Language.English] = "Language_English",
        [Language.French] = "Language_French",
        [Language.German] = "Language_German",
        [Language.Romanian] = "Language_Romanian",
        [Language.Arabic] = "Language_Arabic",
        [Language.Turkish] = "Language_Turkish",
        [Language.Italian] = "Language_Italian",
        [Language.Serbian] = "Language_Serbian",
        [Language.Portuguese] = "Language_Portuguese",
        [Language.Spanish] = "Language_Spanish",
    };
    private List<string> languageTexts;

    public override void Awake()
    {
        base.Awake();
        InitLanguageSelection();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        DisplayLanguageSelection();
    }

    /// <summary>
    /// Display all the avalaible languages
    /// </summary>
    private void InitLanguageSelection()
    {
        languageTexts = languages.Values.ToList();
        LanguagesSelection.ClearOptions();
        List<string> options = ListService.ForEach(languageTexts, languageText => LanguageController.GetText(languageText)).ToList();
        LanguagesSelection.AddOptions(options);
    }

    private void DisplayLanguageSelection()
    {
        LanguagesSelection.value = languageTexts.IndexOf(languages[LanguageController.CurrentLanguage]);
    }

    public void UpdateLanguage()
    {
        // TODO need some comments, do it or you'll regret it later
        LanguageController.SetLanguage(languages.First(pair => pair.Value == languageTexts[LanguagesSelection.value]).Key);
        SetLanguage();
    }
}
