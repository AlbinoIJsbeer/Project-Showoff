using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public string name;
    public int score;

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
        name = "";
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        name = "";
        score = 0;
    }
}
