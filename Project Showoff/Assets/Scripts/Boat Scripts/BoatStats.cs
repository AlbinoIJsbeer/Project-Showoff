using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoatStats : MonoBehaviour
{
    // Delegate to tell the "BoatFuel.cs" to refuel
    public delegate void Refuel();
    public static event Refuel OnRefuel;

    private int score;
    public int Score { get { return score; } set { score = value; } }
    public int money;
    public int Money { get { return money; } set { money = value; } }
    private int trash;
    public int Trash { get { return trash; } set { trash = value; } }
    private int trashCapacityThreshold;
    public int TrashCapacityThreshold { get { return trashCapacityThreshold; } set { trashCapacityThreshold = value; } }

    [SerializeField] private int fuelPrice;
    [SerializeField] private int obstacleHitPenalty;

    // References to UI objects
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private TMP_Text trashDisplay;

    [SerializeField] private GameObject rockHitNotification;
    [SerializeField] private Button deposit;

    [SerializeField] private List<GameObject> facts;
    private int factIndex;

    // Reference to fuel
    private BoatFuel boatFuel;
    private BoatUpgrade boatUpgrade;

    void Start()
    {
        boatFuel = GetComponent<BoatFuel>();
        boatUpgrade = GetComponent<BoatUpgrade>();
        ResetStats();
        factIndex = 0;
    }

    void Update()
    {
        Manager.Instance.Score = score;
        ShowStats();
        ClampScore();
        TrashDebugger();
        ShowFact();
        CheckIfDepositAvailable();
        ClampTrashCapacity();

        if (BoatController.boatCurrentState == BoatController.BoatState.DOCKED)
            rockHitNotification.SetActive(false);

        if (Input.GetKeyDown(KeyCode.H))
            score += 1000;
    }

    private void ClampTrashCapacity()
    {
        trash = Mathf.Clamp(trash, 0, trashCapacityThreshold);
    }

    private void CheckIfDepositAvailable()
    {
        if (trash == 0)
            deposit.interactable = false;
        else
            deposit.interactable = true;
    }

    private void ShowFact()
    {
        if (factIndex == 0 && trash == 50)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 0 && trash == 100 && factIndex == 1)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 1 && trash == 100 && factIndex == 2)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 1 && trash == 200 && factIndex == 3)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 2 && trash == 100 && factIndex == 4)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 2 && trash == 200 && factIndex == 5)
        {
            facts[factIndex].SetActive(true);
            factIndex++;
        }
        else if (boatUpgrade.BoatIndex == 2 && trash == 300 && factIndex == 6)
        {
            facts[factIndex].SetActive(true);
            factIndex = 0;
        }
    }

    // ClampScore
    private void ClampScore()
    {
        score = Mathf.Clamp(score, 0, 99999);
    }

    // Reset stats on new game
    private void ResetStats()
    {
        score = 0;
        money = 200;
        trash = 0;
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
        {
            score -= obstacleHitPenalty;
            rockHitNotification.SetActive(true);
        }
    }
}
