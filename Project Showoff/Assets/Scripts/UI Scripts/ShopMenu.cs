using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    // Boat view in Upgrade Menu
    [SerializeField] private GameObject smallBoatView;
    [SerializeField] private GameObject mediumBoatView;
    [SerializeField] private GameObject largeBoatView;

    // Boat info in Upgrade Menu
    [SerializeField] private GameObject smallBoatInfo;
    [SerializeField] private GameObject mediumBoatInfo;
    [SerializeField] private GameObject largeBoatInfo;

    // Boat stats
    [SerializeField] private GameObject sBoatStats;
    [SerializeField] private GameObject mBoatStats;
    [SerializeField] private GameObject lBoatStats;

    // Prices and availability
    [SerializeField] private GameObject priceS;
    [SerializeField] private GameObject priceM;
    [SerializeField] private GameObject priceL;
    [SerializeField] private GameObject ownedS;
    [SerializeField] private GameObject ownedM;
    [SerializeField] private GameObject ownedL;

    // Insufficient funds
    [SerializeField] private GameObject insufficientS;
    [SerializeField] private GameObject insufficientM;
    [SerializeField] private GameObject insufficientL;
    [SerializeField] private Button upgradeButton;

    private int boatViewIndex = 0;

    // Medium and Large boat costs
    [SerializeField] private int sBoatCost = 5000;
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
        if (boatViewIndex == 0 && boatStats.Money >= sBoatCost)
        {
            boatFuel.MaxFuel = 100;
            boatFuel.Fuel = 100;
            boatStats.Money -= sBoatCost;
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
        // Image
        smallBoatView.SetActive(true);
        mediumBoatView.SetActive(false);
        largeBoatView.SetActive(false);

        // Info
        smallBoatInfo.SetActive(true);
        mediumBoatInfo.SetActive(false);
        largeBoatInfo.SetActive(false);

        if (boatViewIndex == boatUpgrade.BoatIndex)
        {
            priceS.SetActive(false);
            ownedS.SetActive(true);
            upgradeButton.interactable = false;
        }
        else
        {
            priceS.SetActive(true);
            ownedS.SetActive(false);

            // Check if funds are sufficient
            if (boatStats.Money >= sBoatCost)
            {
                sBoatStats.SetActive(true);
                insufficientS.SetActive(false);
                upgradeButton.interactable = true;
            }
            else
            {
                sBoatStats.SetActive(false);
                insufficientS.SetActive(true);
                upgradeButton.interactable = false;
            }
        }
    }

    // Medium boat preview and stats
    private void BoatViewMedium()
    {
        // Image
        smallBoatView.SetActive(false);
        mediumBoatView.SetActive(true);
        largeBoatView.SetActive(false);

        // Info
        smallBoatInfo.SetActive(false);
        mediumBoatInfo.SetActive(true);
        largeBoatInfo.SetActive(false);

        if (boatViewIndex == boatUpgrade.BoatIndex)
        {
            priceM.SetActive(false);
            ownedM.SetActive(true);
            upgradeButton.interactable = false;
        }
        else
        {
            priceM.SetActive(true);
            ownedM.SetActive(false);

            // Check if funds are sufficient
            if (boatStats.Money >= mBoatCost)
            {
                mBoatStats.SetActive(true);
                insufficientM.SetActive(false);
                upgradeButton.interactable = true;
            }
            else
            {
                mBoatStats.SetActive(false);
                insufficientM.SetActive(true);
                upgradeButton.interactable = false;
            }
        }
    }

    // Large boat preview and stats
    private void BoatViewLarge()
    {
        // Image
        smallBoatView.SetActive(false);
        mediumBoatView.SetActive(false);
        largeBoatView.SetActive(true);

        // Info
        smallBoatInfo.SetActive(false);
        mediumBoatInfo.SetActive(false);
        largeBoatInfo.SetActive(true);

        if (boatViewIndex == boatUpgrade.BoatIndex)
        {
            priceL.SetActive(false);
            ownedL.SetActive(true);
            upgradeButton.interactable = false;
        }
        else
        {
            priceL.SetActive(true);
            ownedL.SetActive(false);

            // Check if funds are sufficient
            if (boatStats.Money >= lBoatCost)
            {
                lBoatStats.SetActive(true);
                insufficientL.SetActive(false);
                upgradeButton.interactable = true;
            }
            else
            {
                lBoatStats.SetActive(false);
                insufficientL.SetActive(true);
                upgradeButton.interactable = false;
            }
        }
    }
}
