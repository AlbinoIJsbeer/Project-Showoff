﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoatStats : MonoBehaviour
{
    // Delegate to tell the "BoatFuel.cs" to refuel
    public delegate void Refuel();
    public static event Refuel OnRefuel;

    private int score;
    public int Score { get { return score; } set { score = value; } }
    private int money;
    public int Money { get { return money; } set { money = value; } }
    private int trash;
    public int Trash { get { return trash; } set { trash = value; } }

    [SerializeField] private int fuelPrice = 200;
    [SerializeField] private int obstacleHitPenalty = 200;

    // References to UI objects
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private TMP_Text trashDisplay;

    // Reference to fuel
    private BoatFuel boatFuel;

    void Start()
    {
        boatFuel = GetComponent<BoatFuel>();
    }

    void Update()
    {
        //Manager.Instance.Score = score;
        ShowStats();

        TrashDebugger();
    }

    // Link UI objects to boat stats
    private void ShowStats()
    {
        moneyDisplay.text = money.ToString();
        trashDisplay.text = trash.ToString();
        scoreDisplay.text = score.ToString();
    }

    // Deposit trash in harbor
    public void Deposit()
    {
        score += trash * 10;
        money += trash * 50;
        trash = 0;
    }

    // Buy fuel
    public void BuyFuel()
    {
        if (money >= fuelPrice)
        {
            if (boatFuel.Fuel < boatFuel.MaxFuel)
            {
                OnRefuel?.Invoke();
                money -= fuelPrice;
            }
        }
    }

    // Debugging 
    private void TrashDebugger()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            trash = 50;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            trash = 100;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            trash = 150;
        }
    }

    // If boat collides with obstacles, deduct score
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            score -= obstacleHitPenalty;
    }
}
