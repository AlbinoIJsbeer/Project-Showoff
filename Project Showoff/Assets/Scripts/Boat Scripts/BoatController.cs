using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoatController : MonoBehaviour
{
	// Boat states
	public enum BoatState { START, SAIL, DOCK, DOCKED, RESCUE };
	public static BoatState boatCurrentState = BoatState.SAIL;

	// Target position and angle difference between Boat and target position
	private Vector3 _targetPosition;
	private float _angleDiff;

	// Field of View
	private float FOV = 180f;
	// Navigation distance
	[SerializeField] private float navDist;

	private float timeToClick;

	// Boat speeds
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float boatSpeed;
	[SerializeField] private float maxBoatSpeed;

	private bool _rotateTowardsTarget;

	// For clamping movement
	private Vector3 pos;

	// Access to other scripts attached to the Boat 
	private BoatFuel boatFuel;
	private BoatStats boatStats;
	private BoatUpgrade boatUpgrade;

	// Reference to Dock position for Docking
	[SerializeField] private Transform dock;
	[SerializeField] private GameObject dockMenu;

	[SerializeField] private Vector3 exitDockPosition;
	[SerializeField] private GameObject pointer;
	//[SerializeField] private GameObject rescueNotification;

	[SerializeField] private GameObject birdSave;
	[SerializeField] private GameObject birdDie;

	[SerializeField] private GameObject dust;

	private BoxCollider[] boatColliders;

	private void Start()
	{
		_targetPosition = transform.position;
		boatCurrentState = BoatState.START;
		boatFuel = GetComponent<BoatFuel>();
		boatStats = GetComponent<BoatStats>();
		boatUpgrade = GetComponent<BoatUpgrade>();
		boatColliders = gameObject.GetComponents<BoxCollider>();
		HealthBar.OnBirdRescueSuccess += BirdSaved;
		HealthBar.OnBirdRescueFail += BirdDied;
	}

	private void FixedUpdate()
	{		
		timeToClick -= Time.deltaTime;
		ChangeColliders(boatUpgrade.BoatIndex);
		MaxSpeed(boatUpgrade.BoatIndex);
		ClampBoatPosition(-475, 475, -100, 850);

		switch (boatCurrentState)
		{
			case BoatState.START:
				TargetPosition();
				break;
			// SAIL STATE
			case BoatState.SAIL:
				//PauseMenu.GameIsPaused = false;
				TargetPosition();
				if (NavDistance() && boatFuel.Fuel > 0)
				{
					LookAtTarget();
					Sail();
				}
				break;

			// DOCK STATE
			case BoatState.DOCK:
				Dock();
				break;

			// DOCKED STATE
			case BoatState.DOCKED:
				Docked();
				PauseMenu.GameIsPaused = true;
				break;

			// RESCUE STATE
			case BoatState.RESCUE:
				TargetPosition();
				if (NavDistance() && boatFuel.Fuel > 0)
					RescueAnimal();
				break;
		}
	}

	// Notification for saving bird
	private void BirdSaved()
	{
		birdSave.SetActive(true);
	}

	// Notification for failing to save bird
	private void BirdDied()
	{
		birdDie.SetActive(true);
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
		if (index == 0)
		{
			maxBoatSpeed = 1.0f;
			rotationSpeed = 1.5f;
		}
		else if (index == 1)
		{
			maxBoatSpeed = 0.8f;
			rotationSpeed = 1.25f;
		}
		else if (index == 2)
		{
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

	// NOT USED ANYMORE
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
			if (_angleDiff > 35) boatSpeed = 0.5f;
			else if (_angleDiff > 15) boatSpeed = 0.6f;
			else if (_angleDiff > 7.5f) boatSpeed = 0.7f;
			else boatSpeed = 1f;

			// Use fuel when moving
			boatFuel.Fuel -= 0.02f;
			//boatEngine.volume = 0.4f;
		}
		else
		{
			//boatEngine.volume = 0.1f;
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

		if ((boatUpgrade.BoatIndex == 0 && distance > 12) ||
			(boatUpgrade.BoatIndex == 1 && distance > 25) ||
			(boatUpgrade.BoatIndex == 2 && distance > 32))
		{
			LookAtTarget();
			Sail();
		}
	}

	// Enable click in game after some time when you close the in-game menu
	public void ResetTimerToClick(float sec)
	{
		timeToClick = sec;
	}

	// Set position of boat and rotation after leaving dock
	public void SetStartTarget()
	{
		_targetPosition = transform.position;
		_targetPosition.z += 10;
	}

	// Get clicked position
	private void TargetPosition()
	{
		//float distance;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.tag == "Sea" && boatCurrentState != BoatState.RESCUE)
			{
				if (Input.GetMouseButton(0) && timeToClick <= 0)
				{
					_targetPosition = new Vector3(hit.point.x, 0, hit.point.z);
					boatCurrentState = BoatState.SAIL;
				}
			}
			else if (hit.transform.tag == "Dock")
			{
				if (Input.GetMouseButtonDown(0) && timeToClick <= 0)
				{
					_targetPosition = new Vector3(hit.point.x, 0, hit.point.z);
					boatCurrentState = BoatState.DOCK;
				}
			}
			else if (hit.transform.tag == "Animal")
			{
					if (Input.GetMouseButtonDown(0) && timeToClick <= 0)
					{
						_targetPosition = new Vector3(hit.point.x, 0, hit.point.z);
						boatCurrentState = BoatState.RESCUE;
					}
			}
		}
	}

	// If it collides with dock it enters DOCKED state
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Dock")
		{
			boatCurrentState = BoatState.DOCKED;
		}
		else if (collision.gameObject.tag == "Obstacle")
		{
			FindObjectOfType<AudioManager>().Play("RockHit");
			FindObjectOfType<AudioManager>().Play("PointDeduct");
			foreach (ContactPoint contact in collision.contacts)
			{
				Instantiate(dust, contact.point, Quaternion.identity);
			}
		}
	}
}
