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

    private void Awake()
    {
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
