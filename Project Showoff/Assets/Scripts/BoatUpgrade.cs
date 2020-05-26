using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatUpgrade : MonoBehaviour
{
    GameObject smallBoat;
    GameObject mediumBoat;
    GameObject largeBoat;

    void Start()
    {
        smallBoat = gameObject.transform.GetChild(0).gameObject;
        mediumBoat = gameObject.transform.GetChild(1).gameObject;
        largeBoat = gameObject.transform.GetChild(2).gameObject;
    }

    void Update()
    {
        SwitchBoats();
    }

    void SwitchBoats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            smallBoat.SetActive(true);
            mediumBoat.SetActive(false);
            largeBoat.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            smallBoat.SetActive(false);
            mediumBoat.SetActive(true);
            largeBoat.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            smallBoat.SetActive(false);
            mediumBoat.SetActive(false);
            largeBoat.SetActive(true);
        }
    }
}
