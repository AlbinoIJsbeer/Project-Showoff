using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{
	public delegate void Refuel();
	public static event Refuel OnRefuel;

	public int smallBoatCapacity = 50;
	public int mediumBoatCapacity = 100;
	public int largeBoatCapacity = 150;

	[SerializeField] private TMP_Text scoreDisplay;
	[SerializeField] private TMP_Text moneyDisplay;
	[SerializeField] private TMP_Text trashDisplay;

	private int score = 0;
	public static int money = 0;
	private int trashCollected = 0;

	private int boatIndex;

	void Update()
	{
		boatIndex = ViewSwitch.boatIndex;

		moneyDisplay.text = money.ToString();
		trashDisplay.text = trashCollected.ToString();
		scoreDisplay.text = score.ToString();
	}

	void FixedUpdate()
	{
		Manager.Score = score;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Trash")
		{
			if (boatIndex == 0 && trashCollected < smallBoatCapacity)
			{
				Destroy(other.gameObject);
				trashCollected++;
				score += 10;
			}
			else if (boatIndex == 1 && trashCollected < mediumBoatCapacity)
			{

				Destroy(other.gameObject);
				trashCollected++;
				score += 10;
			}
			else if (boatIndex == 2 && trashCollected < largeBoatCapacity)
			{
				Destroy(other.gameObject);
				trashCollected++;
				score += 10;
			}
		}
	}

	public void Deposit()
	{
		score += trashCollected * 10;
		money += trashCollected * 50;
		trashCollected = 0;
	}

	public void BuyFuel()
	{
		if (money >= 100)
		{
			if (BoatController.fuel < BoatController.maxFuel)
			{
				OnRefuel?.Invoke();
				money -= 100;
			}
		}
	}
}
