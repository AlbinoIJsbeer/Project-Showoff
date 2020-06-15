using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockPointer : MonoBehaviour
{
	[SerializeField] private Transform boat;
	[SerializeField] private Transform dock;

	// Reference for boat type(index)
	private BoatUpgrade boatUpgrade;

	// Get the transform of the pointer
	private Transform pointer;

	// For reposition and resizing 
	private Vector3 tempSize;
	private Vector3 tempPos;

	// Resize rate for the resizing visual effect
	private float resizer = 0.025f;

	// Used for knowing if the pointer should be resized or not based on boat type
	private int prevBoatIndex;
	private bool toSetSize;

	// Pointer distance and size sets for boats
	private Vector3 sizeS = new Vector3(6, 6, 6);
	private Vector3 sizeM = new Vector3(8, 8, 8);
	private Vector3 sizeL = new Vector3(10, 10, 10);
	private float distanceS = 25f;
	private float distanceM = 30f;
	private float distanceL = 45f;

	void Start()
	{
		boatUpgrade = boat.GetComponent<BoatUpgrade>();
		pointer = transform.GetChild(0).GetComponent<Transform>();
		prevBoatIndex = boatUpgrade.BoatIndex;
		toSetSize = false;
	}

	void Update()
	{
		UpdatePosition(boat);
		ChangeSizeThresholds(boatUpgrade.BoatIndex);
		PointAtDock();
	}

	// Makes the pointer follow the boat
	private void UpdatePosition(Transform target)
	{
		transform.position = target.position;
	}

	// Change min and max sizes for the pointer stretching effect based on boat type
	private void ChangeSizeThresholds(int index)
	{
		if (index == 0)
		{
			SetSize(sizeS);
			Position(distanceS);		
			Stretch(4, 8);
			prevBoatIndex = 0;
		}
		else if (index == 1)
		{
			SetSize(sizeM);
			Position(distanceM);
			Stretch(6, 10);
			prevBoatIndex = 1;
		}
		else if (index == 2)
		{
			SetSize(sizeL);
			Position(distanceL);
			Stretch(8, 12);
			prevBoatIndex = 2;
		}
	}

	// Make the pointer point at the dock always
	private void PointAtDock()
	{
		Quaternion targetRotation = Quaternion.LookRotation(dock.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
	}

	// Resize pointer based on boat type
	private void SetSize(Vector3 size)
	{
		if (CheckIfResizePossible())
		{
			pointer.transform.localScale = size;
			toSetSize = false;
		}
	}

	// Stretch the pointer on 'x' axis for visual effects
	private void Stretch(float min, float max)
	{
		tempSize = pointer.transform.localScale;
		tempSize.x += resizer;
		pointer.transform.localScale = tempSize;

		// Change resizer sign
		if (tempSize.x <= min)
			resizer *= -1;
		else if (tempSize.x >= max)
			resizer *= -1;
	}

	// Used for changing pointer position based on boat type
	private void Position(float pos)
	{
		tempPos = pointer.transform.localPosition;
		tempPos.z = pos;
		pointer.transform.localPosition = tempPos;
	}

	// Check if pointer needs to be resized or not based on boat type
	private bool CheckIfResizePossible()
	{
		if (prevBoatIndex != boatUpgrade.BoatIndex)
			toSetSize = true;

		return toSetSize;
	}
}
