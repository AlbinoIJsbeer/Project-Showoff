using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    [SerializeField]
    private GameObject Dutch;
    [SerializeField]
    private GameObject English;
    [SerializeField]
    private GameObject German;

    public void SetLanguageDutch()
    {
        Manager._language = Language.DUTCH;
    }

    public void SetLanguageEnglish()
    {
        Manager._language = Language.ENGLISH;
    }

    public void SetLanguageGerman()
    {
        Manager._language = Language.GERMAN;
    }


}

public enum Language
{
    DUTCH = 0,
    ENGLISH = 1,
    GERMAN = 2
}
