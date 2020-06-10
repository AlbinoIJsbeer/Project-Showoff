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

    private GameObject[] DutchGameObjects;
    private GameObject[] EnglishGameObjects;
    private GameObject[] GermanGameObjects;

    void Awake()
    {

        DutchGameObjects = GameObject.FindGameObjectsWithTag("Dutch");
        EnglishGameObjects = GameObject.FindGameObjectsWithTag("English");
        GermanGameObjects = GameObject.FindGameObjectsWithTag("German");

        Debug.Log(DutchGameObjects.Length);
        Debug.Log(EnglishGameObjects.Length);
        Debug.Log(GermanGameObjects.Length);
    }

    public void SetLanguageDutch()
    {
        Manager.Instance._language = Language.DUTCH;
    }

    public void SetLanguageEnglish()
    {
        Manager.Instance._language = Language.ENGLISH;
    }

    public void SetLanguageGerman()
    {
        Manager.Instance._language = Language.GERMAN;
    }

    void Update()
    {
        switch (Manager.Instance._language)
        {
            case Language.DUTCH:
                foreach (GameObject _object in DutchGameObjects)
                    _object.SetActive(true);
                foreach (GameObject _object in EnglishGameObjects)
                    _object.SetActive(false);
                foreach (GameObject _object in GermanGameObjects)
                    _object.SetActive(false);
                break;
            case Language.ENGLISH:
                foreach (GameObject _object in DutchGameObjects)
                    _object.SetActive(false);
                foreach (GameObject _object in EnglishGameObjects)
                    _object.SetActive(true);
                foreach (GameObject _object in GermanGameObjects)
                    _object.SetActive(false);
                break;
            case Language.GERMAN:
                foreach (GameObject _object in DutchGameObjects)
                    _object.SetActive(false);
                foreach (GameObject _object in EnglishGameObjects)
                    _object.SetActive(false);
                foreach (GameObject _object in GermanGameObjects)
                    _object.SetActive(true);
                break;
        }
    }

}

public enum Language
{
    DUTCH = 0,
    ENGLISH = 1,
    GERMAN = 2
}
