using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
	// Boat states
	public enum BoatState { SAIL, DOCK, DOCKED, RESCUE };
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
	private BoatStats boatStats;
	private BoatUpgrade boatUpgrade;

	// Reference to Dock position for Docking
	[SerializeField] private Transform dock;
	[SerializeField] private GameObject dockMenu;

	[SerializeField] private Vector3 exitDockPosition = new Vector3(125, 0, -10);
	[SerializeField] private GameObject pointer;

	private BoxCollider[] boatColliders;

	private void Start()
	{
		_targetPosition = new Vector3(125, 0, -70);
		boatFuel = GetComponent<BoatFuel>();
		boatStats = GetComponent<BoatStats>();
		boatUpgrade = GetComponent<BoatUpgrade>();
		boatColliders = gameObject.GetComponents<BoxCollider>();
	}

	private void FixedUpdate()
	{
		ChangeColliders(boatUpgrade.BoatIndex);
		MaxSpeed(boatUpgrade.BoatIndex);
		ClampBoatPosition(-350, 350, -100, 600);

		switch (boatCurrentState)
		{
			case BoatState.SAIL:
				//PauseMenu.GameIsPaused = false;
				TargetPosition();
				if (CheckIfTargetPositionWithinFOV() && NavDistance() && boatFuel.Fuel > 0)
				{
					LookAtTarget();
					Sail();					
				}
				break;
			case BoatState.DOCK:
				if (CheckIfTargetPositionWithinFOV())
					Dock();
				else
					boatCurrentState = BoatState.SAIL;
				break;
			case BoatState.DOCKED:
				Docked();
				PauseMenu.GameIsPaused = true;
				break;
			case BoatState.RESCUE:
				TargetPosition();
				if (CheckIfTargetPositionWithinFOV() && NavDistance() && boatFuel.Fuel > 0)
					RescueAnimal();
				break;
		}
	}

	// Change boat colliders based on boat
	private void ChangeColliders(int index)
	{
		if (index == 0)
		{
			boatColliders[0].enabled = true;
			boatColliders[1].enabled = false;
			boatColliders[2].enabled = false;
		}
		else if (index == 1)
		{
			boatColliders[0].enabled = false;
			boatColliders[1].enabled = true;
			boatColliders[2].enabled = false;
		}
		else if (index == 2)
		{
			boatColliders[0].enabled = false;
			boatColliders[1].enabled = false;
			boatColliders[2].enabled = true;
		}

	}

	// Clamps boat speed based on boat type
	private void MaxSpeed(int index)
	{
		if (index == 0) {
			maxBoatSpeed = 1.0f;
			rotationSpeed = 1.5f;
		}
		else if (index == 1) {
			maxBoatSpeed = 0.8f;
			rotationSpeed = 1.25f;
		}
		else if (index == 2) {
			maxBoatSpeed = 0.6f;
			rotationSpeed = 1f;
		}
	}

	// Send boat to dock if boat stuck with no fuel
	public void SendToDock()
	{
		boatCurrentState = BoatState.DOCKED;		
	}

	// Set boat read for sail when undocked
	public void SetSailState()
	{
		boatCurrentState = BoatState.SAIL;
		transform.position = exitDockPosition;
		PauseMenu.GameIsPaused = false;
	}

	// Check if target position is within FOV of the boat
	private bool CheckIfTargetPositionWithinFOV()
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

	// Check if target position is within boat navigation distance
	private bool NavDistance()
	{
		// Check if target position is within navigation distance
		float distanceToTargetPosition = Vector3.Distance(transform.position, _targetPosition);

		if (distanceToTargetPosition < navDist)
			return true;
		else
			return false;
	}

	// Clamps boat movement within the sea boundaries on XZ plane
	private void ClampBoatPosition(int xMin, int xMax, int zMin, int zMax)
	{
		// Clamp boat position so it does not go off the map
		pos = transform.position;
		pos.x = Mathf.Clamp(transform.position.x, xMin, xMax);
		pos.z = Mathf.Clamp(transform.position.z, zMin, zMax);
		transform.position = pos;
	}

	// Make the boat sail
	private void Sail()
	{
		boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, maxBoatSpeed);

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

			// Use fuel when moving
			boatFuel.Fuel -= 0.01f;
		}
		else
		{
			boatSpeed -= 0.01f;
		}
	}

	// Rotates the boat towards the target position
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

	// Moves the boat towards the dock
	private void Dock()
	{
		_targetPosition = new Vector3(dock.transform.position.x, 0, dock.transform.position.z + 35);
		float distance = Vector3.Distance(transform.position, _targetPosition);

		// If near dock then switch to DOCKED state
		if (distance < 50)
		{
			boatCurrentState = BoatState.DOCKED;
		}

		Sail();
		LookAtTarget();
	}

	// Docks the boat and opens the Dock Menu
	private void Docked()
	{
		// Activate Upgrade Menu
		pointer.SetActive(false);
		dockMenu.SetActive(true);
		PauseMenu.GameIsPaused = true;

		// Dock the boat
		transform.position = new Vector3(dock.transform.position.x + 25, 0, dock.transform.position.z + 15);
		transform.rotation = new Quaternion(0, 0, 0, 0);
	}

	// Rescue animal trapped in trash piles
	private void RescueAnimal()
	{
		float distance = Vector3.Distance(transform.position, _targetPosition);

		if (distance > 15)
		{
			LookAtTarget();
			Sail();
		}
		else
		{
			boatStats.Score += 100;
			boatCurrentState = BoatState.SAIL;
		}
	}

	// Get clicked position
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
				else if (hit.transform.tag == "Animal")
				{
					boatCurrentState = BoatState.RESCUE;
				}
			}
		}
	}
}
