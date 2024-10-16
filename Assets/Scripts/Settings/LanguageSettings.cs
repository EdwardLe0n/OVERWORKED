using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSettings : MonoBehaviour
{
    public List<Button> langButtonsList;
    public Color selectedLangColor;
    public Color selectedHighlightColor;

    void Start()
    {
        StartCoroutine(SetDefaultLangButton()); // in order to have correct lang button highlighted on start
    }

    IEnumerator SetDefaultLangButton()
    {
        yield return LocalizationSettings.InitializationOperation;

        int savedLangIndex = PlayerPrefs.GetInt("langKey", -1);
        if (savedLangIndex >= 0 && savedLangIndex < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            // If a valid language index is found in PlayerPrefs, set it as the current locale
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[savedLangIndex];
        }

        var currentLocale = LocalizationSettings.SelectedLocale;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for(int i = 0; i < locales.Count; i++) {
            if(locales[i] == currentLocale) {
                Button defaultButton = langButtonsList[i]; // note: will only work if list is in the same order as the locales
                UpdateButtons(defaultButton);
                break;
            }
        }
    }

    // to show which lang button is currently selected
    public void UpdateButtons(Button clickedButton)
    {
        int selectedIndex = langButtonsList.IndexOf(clickedButton);
        for(int i = 0; i < langButtonsList.Count; i++) {
            ColorBlock colorBlock = langButtonsList[i].colors;
            
            if(i == selectedIndex) {
                // need to update normal and highlighted colors so correct button is highlighted on start
                colorBlock.normalColor = selectedLangColor;
                colorBlock.highlightedColor = selectedHighlightColor;
                colorBlock.selectedColor = selectedLangColor;
                langButtonsList[i].colors = colorBlock;
            }
            else {
                colorBlock.normalColor = Color.white; // default button color
                colorBlock.highlightedColor = new Color(245, 245, 245, 255); // default highlight color
                colorBlock.selectedColor = Color.white; // default button color
                langButtonsList[i].colors = colorBlock;
            }
        }
    }

    public void ChangeLanguage(int langIndex)
    {
        StartCoroutine(SetLocale(langIndex));
    }

    IEnumerator SetLocale(int langIndex)
    {
        yield return LocalizationSettings.InitializationOperation;
        var locales = LocalizationSettings.AvailableLocales.Locales;

        if(langIndex >= 0 && langIndex < locales.Count) {
            LocalizationSettings.SelectedLocale = locales[langIndex];

            PlayerPrefs.SetInt("langKey", langIndex);
            PlayerPrefs.Save(); // save lang as soon as it's updated

            Debug.Log("Language changed to: " + locales[langIndex].Identifier);
        }
        else {
            Debug.Log("invalid lang index");
        }
    }
}
