using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCollector : MonoBehaviour
{
	public int smallBoatCapacity = 50;
	public int mediumBoatCapacity = 100;
	public int largeBoatCapacity = 150;

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
		//ShowPointerOnMaxTrash();
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
			dockPointer.SetActive(true);
		else if (boatUpgrade.BoatIndex == 1 && boatStats.Trash == mediumBoatCapacity)
			dockPointer.SetActive(true);
		else if (boatUpgrade.BoatIndex == 2 && boatStats.Trash == largeBoatCapacity)
			dockPointer.SetActive(true);
		else
			dockPointer.SetActive(false);
	}
}
