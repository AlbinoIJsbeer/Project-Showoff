using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoatControllerV2 : MonoBehaviour
{
	// Boat states
	public enum BoatState { SAIL, DOCK, DOCKED };
	public static BoatState boatCurrentState = BoatState.SAIL;

	// Plane for retrieving mouse clicked position
	static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

	// Target position and angle difference between Boat and target position
	private Vector3 _targetPosition;
	private float _angleDiff;

	// Boat speeds
	public float rotationSpeed = 1.5f;
	public float boatSpeed = 0;
	private bool _rotateTowardsTarget;

	[SerializeField]
	private Transform dock;
	[SerializeField]
	private GameObject upgradeMenu;
	[SerializeField]
	private Button exitUpgradeMenu;

	private void Update()
	{
		switch (boatCurrentState)
		{
			case BoatState.SAIL:
				TargetPosition();
				Sail();
				LookAtTarget();
				upgradeMenu.SetActive(false);
				break;
			case BoatState.DOCK:
				Dock();
				break;
			case BoatState.DOCKED:
				Docked();
				break;
		}
	}

	private void Sail()
	{
		boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, 5.0f);
		float distance = Vector3.Distance(transform.position, _targetPosition);
		transform.position = Vector3.Lerp(transform.position, _targetPosition, boatSpeed * Time.deltaTime);

		if (distance > 1)
			boatSpeed = 1;
		else
			boatSpeed = 0;
		

		// Increase speed while decreasing angle towars target position
		//if (distance > 1)
		//{
		//	if (_angleDiff > 135) boatSpeed += 0.01f;
		//	else if (_angleDiff > 90) boatSpeed += 0.015f;
		//	else if (_angleDiff > 45) boatSpeed += 0.02f;
		//	else boatSpeed += 0.025f;
		//}
		//else
		//{
		//	boatSpeed = 0.02f;
		//}
	}

	private void LookAtTarget()
	{
		// Get target rotation and angle difference
		Quaternion targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
		_angleDiff = Quaternion.Angle(targetRotation, transform.rotation);

		if (_angleDiff > 0.5f) _rotateTowardsTarget = true;
		else _rotateTowardsTarget = false;

		// Rotate towards target rotation unless angle difference is less than 0.5f
		if (_rotateTowardsTarget)
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void Dock()
	{
		_targetPosition = new Vector3(dock.transform.position.x, 0, dock.transform.position.z);
		float distance = Vector3.Distance(transform.position, _targetPosition);
		if (distance < 50)
		{
			boatCurrentState = BoatState.DOCKED;
		}
		Sail();
		LookAtTarget();
	}

	private void Docked()
	{
		// Activate Upgrade Menu
		upgradeMenu.SetActive(true);
		exitUpgradeMenu.onClick.AddListener(delegate { boatCurrentState = BoatState.SAIL; });

		// Dock the boat
		transform.position = _targetPosition;
		transform.rotation = new Quaternion(0, 0, 0, 0);
	}

	private void TargetPosition()
	{
		float distance;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButton(0))
		{
			if (XZPlane.Raycast(ray, out distance))
			{
				// Get clicked position and reset y axis to 0
				_targetPosition = ray.GetPoint(distance);
				_targetPosition.y = 0;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.tag == "Dock")
				{
					boatCurrentState = BoatState.DOCK;
				}
			}
		}
	}
}
