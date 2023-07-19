//using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
//using UnityEngine.SocialPlatforms;

public class Localization : MonoBehaviour
{
    public List<Locale> availableLocals;
    public LocalizationSettings settings;

    // initialize Localization settings
    public void Start()
    {
        settings = LocalizationSettings.Instance;
    }

    // Funktion to change the Language
    public void ChangeLanguage(string countryCode)
    {
        availableLocals = settings.GetAvailableLocales().Locales;
        var language = FindLocalByCountryCode(countryCode);

        if (language != null)
        {
            LocalizationSettings.Instance.SetSelectedLocale(language);
        } 
    }

    // Helper Funktion to find Locale Class by countrycode
    private Locale FindLocalByCountryCode(string countrycode)
    {
        foreach(var locale in availableLocals)
        {
            if (locale.Identifier.CultureInfo.TwoLetterISOLanguageName == countrycode)
            {
                return locale;
            }
        }

        return null;
    }
}
