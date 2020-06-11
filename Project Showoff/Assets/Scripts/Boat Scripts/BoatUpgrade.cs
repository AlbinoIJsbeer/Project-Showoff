using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject smallBoat;
    [SerializeField] private GameObject mediumBoat;
    [SerializeField] private GameObject largeBoat;

    private int boatIndex;
    public int BoatIndex { get { return boatIndex; } set { boatIndex = value; } }

    private void Start()
    {
        boatIndex = 0;      
    }

    void Update()
    {
        DebugUpgrade();
        UpgradeBoat();  
    }

    void UpgradeBoat()
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

    // For debugging purposes
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
