using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Load();
        Debug.Log("Scores locked and loaded!");
    }

}
