using System;
using System.Collections.Generic;
using System.Xml;
using System.Threading.Tasks;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public static LanguageController Instance { get; private set; }

    public Language CurrentLanguage { get; private set; }
    public Dictionary<string, string> LocalizationStrings { get; private set; }
    public List<TextController> TextControllers;

    public void Awake()
    {
        if (Instance != null)
        {
            // TODO Warning
        }
        Instance = this;

        if (PlayerPrefs.HasKey("Language"))
            SetLanguage(PlayerPrefs.GetInt("Language"));
        else
            SetLanguage(GetSystemLanguage());
    }

    private Language GetSystemLanguage(Language DefaultLanguage = Language.English)
    {
        return Enum.IsDefined(typeof(Language), (int)Application.systemLanguage) ? DefaultLanguage : (Language)Application.systemLanguage;
    }

    private void UpdateAllTextControllers()
    {
        foreach (TextController textController in TextControllers)
            textController.UpdateText();
    }

    private async Task<XmlNodeList> LoadLocalizationFile(string fileName)
    {
        XmlDocument localizationFile = await Services.FileService.LoadXml($"LocalizationFiles/{fileName}.xml");
        return localizationFile.SelectNodes($"{fileName}/string");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="language"></param>
    /// <remarks>
    /// <para>Return type is void because it's used isinde a delegate of a dropdown, do not change this</para>
    /// </remarks>
    public async void SetLanguage(Language language)
    {
        // If the language is not defined, trying to load could create many errors
        if (!Enum.IsDefined(typeof(Language), language))
            throw new ArgumentException("I don't speak Klingon");
        CurrentLanguage = language;
        Debug.Log($"DEBUG: Language - CurrentLanguage: {CurrentLanguage}");
        // Save this value for the next time the app is used
        PlayerPrefs.SetInt("Language", (int)CurrentLanguage);
        LocalizationStrings = new Dictionary<string, string>();
        foreach (string fileName in new string[] { "General", CurrentLanguage.ToString() })
        {
            foreach (XmlNode node in await LoadLocalizationFile(fileName))
            {
                // "name" is the attribute containing the localization string in the xml file
                LocalizationStrings.Add(node.Attributes["name"].Value, node.InnerText);
            }
        }
        UpdateAllTextControllers();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="languageIndex">Index of the selected language inside the LAnguage enum</param>
    /// <remarks>
    /// <para>Return type is void because it's used isinde a delegate of a dropdown, do not change this</para>
    /// </remarks>
    public void SetLanguage(int languageIndex)
    {
        SetLanguage((Language)languageIndex);
    }

    public string GetText(string localizationString)
    {
        if (!LocalizationStrings.ContainsKey(localizationString))
        {
            // If there is no translation available
            // This could be due to the translation not being loaded yet
            Debug.LogWarning($"WARNING: Language - Missing translation for {localizationString}");
            return localizationString;
        }
        else
            //
            return LocalizationStrings[localizationString];
    }

    public string GetLocalizationString(string text)
    {
        foreach (KeyValuePair<string, string> pair in LocalizationStrings)
        {
            if (pair.Value == text)
                return pair.Key;
        }
        return null;
    }
}
