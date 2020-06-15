using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    // Boat view in Upgrade Menu
    [SerializeField] private GameObject smallBoatView;
    [SerializeField] private GameObject mediumBoatView;
    [SerializeField] private GameObject largeBoatView;

    // Boat stats in Upgrade Menu
    [SerializeField] private GameObject smallBoatInfo;
    [SerializeField] private GameObject mediumBoatInfo;
    [SerializeField] private GameObject largeBoatInfo;

    private int boatViewIndex = 0;

    // Medium and Large boat costs
    [SerializeField] private int mBoatCost = 10000;
    [SerializeField] private int lBoatCost = 20000;

    // Reference to boat index, stats and fuel
    [SerializeField] private BoatFuel boatFuel;
    [SerializeField] private BoatStats boatStats;
    [SerializeField] private BoatUpgrade boatUpgrade;

    private void Start()
    {
        boatViewIndex = 0;
    }

    private void Update()
    {
        UpdatePreview(boatViewIndex);
    }

    // For the "Next Button" in Upgrade Menu
    public void Next()
    {
        if (boatViewIndex == 2)
            boatViewIndex = 0;
        else
            boatViewIndex++;
    }

    // For the "Previous Button" in Upgrade Menu
    public void Previous()
    {
        if (boatViewIndex == 0)
            boatViewIndex = 2;
        else
            boatViewIndex--;
    }

    // For the "Upgrade Button" in Upgrade Menu
    public void Upgrade()
    {
        if (boatViewIndex == 0)
        {
            boatUpgrade.BoatIndex = 0;
        }
        else if (boatViewIndex == 1 && boatStats.Money >= mBoatCost)
        {
            boatFuel.MaxFuel = 150;
            boatFuel.Fuel = 150;        
            boatStats.Money -= mBoatCost;
            boatUpgrade.BoatIndex = 1;
        }
        else if (boatViewIndex == 2 && boatStats.Money >= lBoatCost)
        {
            boatFuel.MaxFuel = 200;
            boatFuel.Fuel = 200;
            boatStats.Money -= lBoatCost;
            boatUpgrade.BoatIndex = 2;
        }
    }

    // Update the shop view
    private void UpdatePreview(int index)
    {
        if (index == 0)
            BoatViewSmall();
        else if (index == 1)
            BoatViewMedium();
        else if (index == 2)
            BoatViewLarge();
    }

    // Small boat preview and stats
    private void BoatViewSmall()
    {
        smallBoatView.SetActive(true);
        mediumBoatView.SetActive(false);
        largeBoatView.SetActive(false);
        smallBoatInfo.SetActive(true);
        mediumBoatInfo.SetActive(false);
        largeBoatInfo.SetActive(false);
    }

    // Medium boat preview and stats
    private void BoatViewMedium()
    {
        smallBoatView.SetActive(false);
        mediumBoatView.SetActive(true);
        largeBoatView.SetActive(false);
        smallBoatInfo.SetActive(false);
        mediumBoatInfo.SetActive(true);
        largeBoatInfo.SetActive(false);
    }

    // Large boat preview and stats
    private void BoatViewLarge()
    {
        smallBoatView.SetActive(false);
        mediumBoatView.SetActive(false);
        largeBoatView.SetActive(true);
        smallBoatInfo.SetActive(false);
        mediumBoatInfo.SetActive(false);
        largeBoatInfo.SetActive(true);
    }
}
