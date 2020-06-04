using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitch : MonoBehaviour
{
    [SerializeField] private GameObject smallBoatView;
    [SerializeField] private GameObject mediumBoatView;
    [SerializeField] private GameObject largeBoatView;

    [SerializeField] private GameObject smallBoatInfo;
    [SerializeField] private GameObject mediumBoatInfo;
    [SerializeField] private GameObject largeBoatInfo;

    private int index = 0;
    public static int boatIndex = 0;

    private void Update()
    {
        if (index == 0)
        {
            smallBoatView.SetActive(true);
            mediumBoatView.SetActive(false);
            largeBoatView.SetActive(false);

            smallBoatInfo.SetActive(true);
            mediumBoatInfo.SetActive(false);
            largeBoatInfo.SetActive(false);
        }
        else if (index == 1)
        {
            smallBoatView.SetActive(false);
            mediumBoatView.SetActive(true);
            largeBoatView.SetActive(false);

            smallBoatInfo.SetActive(false);
            mediumBoatInfo.SetActive(true);
            largeBoatInfo.SetActive(false);
        }
        else if (index == 2)
        {
            smallBoatView.SetActive(false);
            mediumBoatView.SetActive(false);
            largeBoatView.SetActive(true);

            smallBoatInfo.SetActive(false);
            mediumBoatInfo.SetActive(false);
            largeBoatInfo.SetActive(true);
        }
    }

    public void Next()
    {
        if (index == 2)
            index = 0;
        else
            index++;
    }

    public void Previous()
    {
        if (index == 0)
            index = 2;
        else
            index--;
    }

    public void Upgrade()
    {
        if (index == 0)
            boatIndex = 0;
        else if (index == 1)
            boatIndex = 1;
        else if (index == 2)
            boatIndex = 2;
    }
}
