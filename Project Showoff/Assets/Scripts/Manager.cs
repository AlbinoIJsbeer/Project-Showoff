using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public string Name { get; set; }
    public int Score;

    
    public Language _language = Language.ENGLISH;

    private void Awake()
    {

        Name = "Anonymous";
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


    public void SetPlayerNickName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is empty");
            return;
        }
        Name = value;
        Debug.Log(Name);
    }


}
