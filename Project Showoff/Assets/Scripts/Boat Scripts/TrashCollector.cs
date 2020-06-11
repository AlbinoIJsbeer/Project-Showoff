using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{
	[SerializeField] private TMP_Text capacityThreshold;
	[SerializeField] private GameObject capacityNotification;
	private float notificationTimer = 5;
	private bool notificationOn = false;

	public int smallBoatCapacity = 50;
	public int mediumBoatCapacity = 100;
	public int largeBoatCapacity = 150;

	private int currentCapacityThreshold;

	private BoatStats boatStats;
	private BoatUpgrade boatUpgrade;

	[SerializeField] private GameObject dockPointer;

	private void Start()
	{
		boatStats = GetComponent<BoatStats>();
		boatUpgrade = GetComponent<BoatUpgrade>();
	}

	private void Update()
	{	
		ChangeCapacityThreshold();
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
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += 10;
			}
			else if (boatUpgrade.BoatIndex == 1 && boatStats.Trash < mediumBoatCapacity)
			{
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += 10;
			}
			else if (boatUpgrade.BoatIndex == 2 && boatStats.Trash < largeBoatCapacity)
			{
				Destroy(other.gameObject);
				boatStats.Trash++;
				boatStats.Score += 10;
			}
		}
	}

	private void ShowPointerOnMaxTrash()
	{
		if (boatUpgrade.BoatIndex == 0 && boatStats.Trash == smallBoatCapacity)
		{
			dockPointer.SetActive(true);
			if (notificationOn == false)
			{
				capacityNotification.SetActive(true);
				notificationOn = true;
			}
		}
		else if (boatUpgrade.BoatIndex == 1 && boatStats.Trash == mediumBoatCapacity)
		{
			dockPointer.SetActive(true);
			if (notificationOn == false)
			{
				capacityNotification.SetActive(true);
				notificationOn = true;
			}
		}
		else if (boatUpgrade.BoatIndex == 2 && boatStats.Trash == largeBoatCapacity)
		{
			dockPointer.SetActive(true);
			if (notificationOn == false)
			{
				capacityNotification.SetActive(true);
				notificationOn = true;
			}
		}
		else
			dockPointer.SetActive(false);	
	}

	private void ChangeCapacityThreshold()
	{
		if (boatUpgrade.BoatIndex == 0)
			currentCapacityThreshold = smallBoatCapacity;
		else if (boatUpgrade.BoatIndex == 1)
			currentCapacityThreshold = mediumBoatCapacity;
		else if (boatUpgrade.BoatIndex == 2)
			currentCapacityThreshold = largeBoatCapacity;
	}

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
