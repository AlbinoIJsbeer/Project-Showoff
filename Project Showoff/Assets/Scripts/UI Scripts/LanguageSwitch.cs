using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitch : MonoBehaviour
{
    [SerializeField] private GameObject english;
    [SerializeField] private GameObject dutch;
    [SerializeField] private GameObject german;

    private Language currentLanguage;

    void Update()
    {
        // DONT FORGET LANGUAGE
        currentLanguage = Manager.Instance._language;

        if (currentLanguage == Language.ENGLISH)
        {
            english.SetActive(true);
            dutch.SetActive(false);
            german.SetActive(false);
        }
        else if (currentLanguage == Language.DUTCH)
        {
            english.SetActive(false);
            dutch.SetActive(true);
            german.SetActive(false);
        }
        else if (currentLanguage == Language.GERMAN)
        {
            english.SetActive(false);
            dutch.SetActive(false);
            german.SetActive(true);
        }
    }
}
