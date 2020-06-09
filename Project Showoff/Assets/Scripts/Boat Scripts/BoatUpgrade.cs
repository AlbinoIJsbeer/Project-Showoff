using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject smallBoat;
    [SerializeField] private GameObject mediumBoat;
    [SerializeField] private GameObject largeBoat;

    public static int boatIndex;

    void Update()
    {
        boatIndex = ViewSwitch.boatIndex;
        DebugUpgrade();
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

    void DebugUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            boatIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            boatIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            boatIndex = 2;
        }
    }
}
