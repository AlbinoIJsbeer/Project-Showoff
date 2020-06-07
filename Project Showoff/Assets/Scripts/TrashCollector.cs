using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{
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
            if (boatIndex == 0 && trashCollected < 50)
            {
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
            else if (boatIndex == 1 && trashCollected < 100)
            {

                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
            else if (boatIndex == 2 && trashCollected < 200)
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
