using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public List<string> Languages;

    public string CurrentLanguage { get; private set; }
    public Dictionary<string, string> LocalizationStrings { get; private set; }

    public void Awake()
    {
        // TODO temp value for testing purposes
        SetLanguage("French");
    }

    private XmlNodeList LoadLocalizationFile(string fileName)
    {
        XmlDocument localizationFile = Services.FileService.LoadXml($"LocalizationFiles/{fileName}.xml").Result;
        return localizationFile.SelectNodes($"{fileName}/string");
    }

    public void SetLanguage(string language)
    {
        CurrentLanguage = language;
        LocalizationStrings = new Dictionary<string, string>();
        foreach (string fileName in new string[] { "General", CurrentLanguage })
        {
            foreach (XmlNode node in LoadLocalizationFile(fileName))
            {
                LocalizationStrings.Add(node.Attributes["name"].Value, node.InnerText);
            }
        }
    }

    public void SetLanguage(int languageIndex)
    {
        SetLanguage(Languages[languageIndex]);
    }

    public string GetText(string localizationString)
    {
        if (!LocalizationStrings.ContainsKey(localizationString))
        {
            // If there is no translation available
            Debug.LogWarning($"WARNING: Missing translation for {localizationString}");
            return localizationString;
        }
        else
            //
            return LocalizationStrings[localizationString];
    }
}
