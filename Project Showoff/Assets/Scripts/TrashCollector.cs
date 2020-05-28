using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
<<<<<<< HEAD
{
    public TMP_Text trashDisplay;
    public static int trashCollected = 0;
    public TMP_Text trashRecycledDisplay;
    public static int trashRecycled = 0;
    public TMP_Text scoreDisplay;
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
=======
{ 
    public TMP_Text scoreDisplay;
    public TMP_Text trashDisplay;
    public TMP_Text trashRecycledDisplay;

    public static int score = 0;
    public static int trashCollected = 0;
    public static int trashRecycled = 0;
    
>>>>>>> DrilonBranch
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
<<<<<<< HEAD
            if (trashCollected <= 50)
            {
                Debug.Log("Collied with Trash");
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
=======
            //if (trashCollected <= 50)
            //{
                //Debug.Log("Collied with Trash");
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            //}
>>>>>>> DrilonBranch
        }
    }
}
