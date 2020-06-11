using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockPointer : MonoBehaviour
{
	[SerializeField] private Transform boat;
	[SerializeField] private Transform dock;

	private BoatUpgrade boatUpgrade;
	private Transform pointer;

	private Vector3 tempSize;
	private Vector3 tempPos;
	//private float minSize = 6f;
	//private float maxSize = 10f;
	private float resizer;

	private int prevBoatIndex;
	private bool toSetSize;

	[SerializeField] private Vector3 sizeS = new Vector3(6, 6, 6);
	[SerializeField] private Vector3 sizeM = new Vector3(8, 8, 8);
	[SerializeField] private Vector3 sizeL = new Vector3(10, 10, 10);
	[SerializeField] private float distanceS = 25f;
	[SerializeField] private float distanceM = 30f;
	[SerializeField] private float distanceL = 45f;

	void Start()
	{
		boatUpgrade = boat.GetComponent<BoatUpgrade>();
		pointer = transform.GetChild(0).GetComponent<Transform>();
		resizer = 0.025f;
		prevBoatIndex = boatUpgrade.BoatIndex;
		toSetSize = false;
	}

	void Update()
	{
		transform.position = boat.position;
		ChangeSizeThresholds();
		PointAtDock();
	}

	private void ChangeSizeThresholds()
	{
		if (boatUpgrade.BoatIndex == 0)
		{
			SetSize(sizeS);
			Position(distanceS);		
			Resize(4, 8);
			prevBoatIndex = 0;
		}
		else if (boatUpgrade.BoatIndex == 1)
		{
			SetSize(sizeM);
			Position(distanceM);
			Resize(6, 10);
			prevBoatIndex = 1;
		}
		else if (boatUpgrade.BoatIndex == 2)
		{
			SetSize(sizeL);
			Position(distanceL);
			Resize(8, 12);
			prevBoatIndex = 2;
		}
	}

	private void PointAtDock()
	{
		Quaternion targetRotation = Quaternion.LookRotation(dock.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
	}

	private void SetSize(Vector3 size)
	{
		if (CheckIfResizePossible())
		{
			pointer.transform.localScale = size;
			toSetSize = false;
		}
	}

	private void Resize(float min, float max)
	{
		tempSize = pointer.transform.localScale;
		tempSize.x += resizer;
		pointer.transform.localScale = tempSize;

		if (tempSize.x <= min)
			resizer = resizer * -1;
		else if (tempSize.x >= max)
			resizer = resizer * -1;
	}

	private void Position(float pos)
	{
		tempPos = pointer.transform.localPosition;
		tempPos.z = pos;
		pointer.transform.localPosition = tempPos;
	}

	private bool CheckIfResizePossible()
	{
		if (prevBoatIndex != boatUpgrade.BoatIndex)
			toSetSize = true;

		return toSetSize;
	}
}
