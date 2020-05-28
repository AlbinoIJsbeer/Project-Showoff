using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

<<<<<<< HEAD
    public string name;
    public int score;
=======
    //public string name;
    //public int score;
>>>>>>> DrilonBranch

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
<<<<<<< HEAD
        name = "";
        score = 0;
=======

>>>>>>> DrilonBranch
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
<<<<<<< HEAD
        name = "";
        score = 0;
=======
        //name = "";
        //score = 0;
>>>>>>> DrilonBranch
    }
}
