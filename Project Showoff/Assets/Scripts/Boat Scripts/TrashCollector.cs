using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{
    public int smallBoatCapacity = 50;
    public int mediumBoatCapacity = 100;
    public int largeBoatCapacity = 150;

    public TMP_Text scoreDisplay;
    public TMP_Text moneyDisplay;
    public TMP_Text trashDisplay;

    public static int score = 0;
    private int money = 0;
    private int trashCollected = 0;

    private int boatIndex;
    
    void Update()
    {
        boatIndex = ViewSwitch.boatIndex;
        //moneyDisplay.text = money.ToString();
        trashDisplay.text = trashCollected.ToString();
        scoreDisplay.text = score.ToString();

        EmptyBoatOnDock();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            if (boatIndex == 0 && trashCollected < smallBoatCapacity)
            {
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
            else if (boatIndex == 1 && trashCollected < mediumBoatCapacity)
            {

                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
            else if (boatIndex == 2 && trashCollected < largeBoatCapacity)
            {
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
        }
    }

    private void EmptyBoatOnDock()
    {
        if (BoatController.boatCurrentState == BoatController.BoatState.DOCKED)
        {
            score += trashCollected * 100;
            trashCollected = 0;
        }
    }
}
