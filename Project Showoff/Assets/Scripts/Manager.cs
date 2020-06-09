using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public static string Name { get; set; }
    public static int Score;

    public static Language _language = Language.ENGLISH;

    private GameObject[] DutchGameObjects;
    private GameObject[] EnglishGameObjects;
    private GameObject[] GermanGameObjects;

    private void Awake()
    {

        DutchGameObjects = GameObject.FindGameObjectsWithTag("Dutch");
        EnglishGameObjects = GameObject.FindGameObjectsWithTag("English");
        GermanGameObjects = GameObject.FindGameObjectsWithTag("German");

        Debug.Log(DutchGameObjects.Length);
        Debug.Log(EnglishGameObjects.Length);
        Debug.Log(GermanGameObjects.Length);

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {

    }

    
    void Update()
    {
        switch (_language)
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

    public void SetPlayerNickName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is empty");
            return;
        }
        Name = value;
        Manager.Name = Name;
        Debug.Log(Manager.Name);
    }

    private void Reset()
    {
        //name = "";
        //score = 0;
    }
}
