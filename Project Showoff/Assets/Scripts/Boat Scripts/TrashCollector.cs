using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class TrashCollector : MonoBehaviour
{
	// Notification for maximum trash capacity reached
	[SerializeField] private TMP_Text capacityThreshold;
	[SerializeField] private GameObject capacityNotification;
	private float notificationTimer = 5;
	private bool notificationOn = false;

	// Score increase per trash collected
	private int scoreIncrement = 10;

	// Dock pointer
	[SerializeField] private GameObject dockPointer;

	// Boat trash capacities
	public int smallBoatCapacity;
	public int mediumBoatCapacity;
	public int largeBoatCapacity;

	// Current boat trash capacity
	private int currentCapacityThreshold;

	// Reference to boat index and stats
	private BoatStats boatStats;
	private BoatUpgrade boatUpgrade;

	private void Start()
	{
		boatStats = GetComponent<BoatStats>();
		boatUpgrade = GetComponent<BoatUpgrade>();
	}

	private void Update()
	{	
		ChangeCapacityThreshold(boatUpgrade.BoatIndex);
		ShowPointerOnMaxTrash();
		capacityThreshold.text = "/" + currentCapacityThreshold.ToString();
		RemoveNotificationOnTime();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Trash")
		{
			if (boatUpgrade.BoatIndex == 0 && boatStats.Trash < smallBoatCapacity)
			{
				FindObjectOfType<AudioManager>().Play("Collect");
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += scoreIncrement;
			}
			else if (boatUpgrade.BoatIndex == 1 && boatStats.Trash < mediumBoatCapacity)
			{
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += scoreIncrement;
			}
			else if (boatUpgrade.BoatIndex == 2 && boatStats.Trash < largeBoatCapacity)
			{
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += scoreIncrement;
			}
		}
	}

	// Show pointer when trash max capacity is reached
	private void ShowPointerOnMaxTrash()
	{
		if (dockPointer.activeSelf == false)
		{
			if (BoatController.boatCurrentState == BoatController.BoatState.DOCKED)
				dockPointer.SetActive(false);
			else if (boatUpgrade.BoatIndex == 0 && boatStats.Trash == smallBoatCapacity)
				ShowPointer();
			else if (boatUpgrade.BoatIndex == 1 && boatStats.Trash == mediumBoatCapacity)
				ShowPointer();
			else if (boatUpgrade.BoatIndex == 2 && boatStats.Trash == largeBoatCapacity)
				ShowPointer(); 
		}
	}

	// Show Pointer
	private void ShowPointer()
	{
		dockPointer.SetActive(true);
		if (notificationOn == false)
		{
			capacityNotification.SetActive(true);
			notificationOn = true;
		}
	}

	// Change capactiy threshold based on boat 
	private void ChangeCapacityThreshold(int index)
	{
		if (index == 0)
			currentCapacityThreshold = smallBoatCapacity;
		else if (index == 1)
			currentCapacityThreshold = mediumBoatCapacity;
		else if (index == 2)
			currentCapacityThreshold = largeBoatCapacity;
	}

	// Remove trash max capacity notification
	private void RemoveNotificationOnTime()
	{
		if (boatStats.Trash == 0)
			notificationOn = false;

		if (capacityNotification.activeSelf)
		{
			notificationTimer -= Time.deltaTime;

			if (notificationTimer <= 0)
			{
				capacityNotification.SetActive(false);
				notificationTimer = 5;
			}
		}
	}
}
