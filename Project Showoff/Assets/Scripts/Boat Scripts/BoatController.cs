using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
	// Boat states
	public enum BoatState { SAIL, DOCK, DOCKED };
	public static BoatState boatCurrentState = BoatState.SAIL;

	// Plane for retrieving mouse clicked position
	static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

	// Target position and angle difference between Boat and target position
	private Vector3 _targetPosition;
	private float _angleDiff;

	// Field of View
	private float FOV = 180f;

	// Boat speeds
	public float rotationSpeed = 1.5f;
	public float boatSpeed = 0;
	private bool _rotateTowardsTarget;

	// For clamping movement
	private Vector3 pos;

	// Navigation distance
	public float navDist = 75f;
	
	public float maxFuel = 100;
	public float fuel = 100;

	[SerializeField]
	private Transform fuelGaugeNeedle;

	[SerializeField]
	private Transform dock;
	[SerializeField]
	private GameObject upgradeMenu;
	[SerializeField]
	private Button exitUpgradeMenu;

	private void Start()
	{
		//fuel = 100;
	}

	private void Update()
	{
		FuelGauge();
		ClampBoatPosition();

		switch (boatCurrentState)
		{
			case BoatState.SAIL:
				TargetPosition();
				if (CheckIfTargetPositionInFront() && NavDistance())
				{
					Sail();
					LookAtTarget();				
				}
				upgradeMenu.SetActive(false);
				break;
			case BoatState.DOCK:
				if (CheckIfTargetPositionInFront())
				{
					Dock();
				}
				else
					boatCurrentState = BoatState.SAIL;
				break;
			case BoatState.DOCKED:
				Docked();
				break;
		}

		
	}

	private void FuelGauge()
	{
		fuel = Mathf.Clamp(fuel, 0, maxFuel);
		float fuelPercentage = fuel / maxFuel;
		fuelGaugeNeedle.transform.rotation = Quaternion.Euler(0, 0, (90 - (180 * fuelPercentage)));
	}

	private bool CheckIfTargetPositionInFront()
	{
		// Check if the target position is within the field of view
		float halfFOV = FOV / 2;
		float halfFOVcos = Mathf.Cos(halfFOV);
		float DOTProduct = Vector3.Dot(transform.forward, _targetPosition - transform.position);
		float MultiplicationProduct = Vector3.Magnitude(transform.forward) * Vector3.Magnitude(_targetPosition - transform.position);
		float thresholdAngle = DOTProduct / MultiplicationProduct;

		if (thresholdAngle >= halfFOVcos)
			return true;
		else
			return false;
	}

	private bool NavDistance()
	{
		// Check if target position is within navigation distance
		float distanceToTargetPosition = Vector3.Distance(transform.position, _targetPosition);

		if (distanceToTargetPosition < navDist)
			return true;
		else
			return false;
	}

	private void ClampBoatPosition()
	{
		// Clamp boat position so it does not go off the map
		pos = transform.position;
		pos.x = Mathf.Clamp(transform.position.x, -350, 350);
		pos.z = Mathf.Clamp(transform.position.z, -100, 600);
		transform.position = pos;
	}

	private void Sail()
	{
		boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, 5.0f);

		//Version 2
		// Boat moves only forward while turning as well
		Vector3 heading = _targetPosition - transform.position;
		Vector3 force = Vector3.Project(heading, transform.forward);
		Vector3 tempPosition = transform.position + force;
		transform.position = Vector3.Slerp(transform.position, tempPosition, boatSpeed * Time.deltaTime);

		float distance = Vector3.Distance(transform.position, tempPosition);

		// Increase speed while the angle between the boat and target position gets smaller
		// If boat is on the target position then turn speed down to 0
		if (distance > 1)
		{
			if (_angleDiff > 35) boatSpeed = 0.7f;
			else if (_angleDiff > 15) boatSpeed = 0.8f;
			else if (_angleDiff > 7.5f) boatSpeed = 0.9f;
			else boatSpeed = 1f;

			fuel -= 0.01f;
		}
		else
		{
			boatSpeed = 0;
		}
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

		// If near dock then switch to DOCKED state
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
