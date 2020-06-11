using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoatStats : MonoBehaviour
{
    public delegate void Refuel();
    public static event Refuel OnRefuel;

    private int score;
    public int Score { get { return score; } set { score = value; } }
    public int money;
    public int Money { get { return money; } set { money = value; } }
    private int trashCollected;
    public int Trash { get { return trashCollected; } set { trashCollected = value; } }

    [SerializeField] private int fuelPrice = 100;

    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private TMP_Text trashDisplay;

    private BoatFuel boatFuel;

    void Start()
    {
        boatFuel = GetComponent<BoatFuel>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = money.ToString();
        trashDisplay.text = trashCollected.ToString();
        scoreDisplay.text = score.ToString();

        TrashDebugger();
    }

    public void Deposit()
    {
        score += trashCollected * 10;
        money += trashCollected * 50;
        trashCollected = 0;
    }

    public void BuyFuel()
    {
        if (money >= fuelPrice)
        {
            if (boatFuel.Fuel < boatFuel.MaxFuel)
            {
                OnRefuel?.Invoke();
                money -= 100;
            }
        }
    }

    private void TrashDebugger()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            trashCollected = 50;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            trashCollected = 100;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            trashCollected = 150;
        }
    }
}
