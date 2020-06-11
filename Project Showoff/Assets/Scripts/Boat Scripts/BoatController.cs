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
	public float maxBoatSpeed = 1f;
	private bool _rotateTowardsTarget;

	// For clamping movement
	private Vector3 pos;

	// Navigation distance
	public float navDist = 75f;

	// Access to other scripts attached to the Boat 
	private BoatFuel boatFuel;
	private BoatUpgrade boatUpgrade;

	// Reference to Dock position for Docking
	[SerializeField] private Transform dock;
	[SerializeField] private GameObject dockMenu;

	private void Start()
	{
		boatFuel = GetComponent<BoatFuel>();
		boatUpgrade = GetComponent<BoatUpgrade>();
	}

	private void Update()
	{
		MaxSpeed();
		ClampBoatPosition();

		switch (boatCurrentState)
		{
			case BoatState.SAIL:
				Time.timeScale = 1;
				TargetPosition();
				if (CheckIfTargetPositionInFront() && NavDistance() && boatFuel.Fuel > 0)
				{
					Sail();
					LookAtTarget();				
				}
				break;
			case BoatState.DOCK:
				if (CheckIfTargetPositionInFront())
					Dock();
				else
					boatCurrentState = BoatState.SAIL;
				break;
			case BoatState.DOCKED:
				Docked();
				break;
		}
	}

	private void MaxSpeed()
	{
		if (boatUpgrade.BoatIndex == 0)
		{
			maxBoatSpeed = 1.0f;
			rotationSpeed = 1.5f;
		}
		else if (boatUpgrade.BoatIndex == 1)
		{
			maxBoatSpeed = 0.8f;
			rotationSpeed = 1.25f;
		}
		else if (boatUpgrade.BoatIndex == 2)
		{
			maxBoatSpeed = 0.6f;
			rotationSpeed = 1f;
		}
	}
	
	public void SetSailState()
	{
		boatCurrentState = BoatState.SAIL;
		transform.position = new Vector3(125, 0, -10);
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
		boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, maxBoatSpeed);

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

			boatFuel.Fuel -= 0.01f;
		}
		else
		{
			boatSpeed -= 0.01f;
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
		_targetPosition = new Vector3(dock.transform.position.x + 25, 0, dock.transform.position.z + 15);
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
		dockMenu.SetActive(true);
		Time.timeScale = 0;

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
