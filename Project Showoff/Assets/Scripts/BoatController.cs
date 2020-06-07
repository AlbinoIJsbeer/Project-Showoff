﻿using System.Collections;
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
	private float FOV = 90f;

	// Boat speeds
	public float rotationSpeed = 1.5f;
	public float boatSpeed = 0;
	private bool _rotateTowardsTarget;

	// For clamping movement
	private Vector3 pos;

	[SerializeField]
	private Transform dock;
	[SerializeField]
	private GameObject upgradeMenu;
	[SerializeField]
	private Button exitUpgradeMenu;

	private void Update()
	{
		ClampBoatPosition();

		switch (boatCurrentState)
		{
			case BoatState.SAIL:
				TargetPosition();
				if (CheckIfTargetPositionInFront())
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

	private bool CheckIfTargetPositionInFront()
	{
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

	private void ClampBoatPosition()
	{
		pos = transform.position;
		pos.x = Mathf.Clamp(transform.position.x, -350, 350);
		pos.z = Mathf.Clamp(transform.position.z, -100, 600);
		transform.position = pos;
	}

	private void Sail()
	{
		boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, 5.0f);

		// Version 1
		//float distance = Vector3.Distance(transform.position, _targetPosition);
		//transform.position = Vector3.Lerp(transform.position, _targetPosition, boatSpeed * Time.deltaTime);

		//if (distance > 1)
		//	boatSpeed = 1;
		//else
		//	boatSpeed = 0;

		//Version 2
		Vector3 heading = _targetPosition - transform.position;
		Vector3 force = Vector3.Project(heading, transform.forward);
		Vector3 tempPosition = transform.position + force;

		float distanceT = Vector3.Distance(transform.position, tempPosition);
		transform.position = Vector3.Lerp(transform.position, tempPosition, boatSpeed * Time.deltaTime);


		if (distanceT > 1)
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
