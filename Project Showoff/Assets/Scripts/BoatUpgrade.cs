using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject smallBoat;
    [SerializeField] private GameObject mediumBoat;
    [SerializeField] private GameObject largeBoat;

    private int boatIndex;

    void Update()
    {
        boatIndex = ViewSwitch.boatIndex;
        SwitchBoats();
    }

    void SwitchBoats()
    {
        if (boatIndex == 0)
        {
            smallBoat.SetActive(true);
            mediumBoat.SetActive(false);
            largeBoat.SetActive(false);     
        }
        else if (boatIndex == 1)
        {
            smallBoat.SetActive(false);
            mediumBoat.SetActive(true);
            largeBoat.SetActive(false);
        }
        else if (boatIndex == 2)
        {
            smallBoat.SetActive(false);
            mediumBoat.SetActive(false);
            largeBoat.SetActive(true);
        }
    }
}
