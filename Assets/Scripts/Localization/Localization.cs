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
    public void ChangeLanguage(string countryCode)
    {
        availableLocals = LocalizationSettings.AvailableLocales.Locales;
        var language = FindLocalByCountryCode(countryCode);

        if (language != null)
        {
            
        } 
    }

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
