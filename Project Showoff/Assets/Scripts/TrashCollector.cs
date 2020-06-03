﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{ 
    public TMP_Text scoreDisplay;
    public TMP_Text trashDisplay;
    public TMP_Text trashRecycledDisplay;

    public static int score = 0;
    public static int trashCollected = 0;
    public static int trashRecycled = 0;
    
    void Update()
    {
        trashDisplay.text = trashCollected.ToString();
        trashRecycledDisplay.text = trashRecycled.ToString();
        scoreDisplay.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            //if (trashCollected <= 50)
            //{
                //Debug.Log("Collied with Trash");
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            //}
        }
    }
}