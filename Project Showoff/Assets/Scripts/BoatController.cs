using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BoatController : MonoBehaviour
{
    // For mouse click position
    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

    private Vector3 _targetPosition;
    private float _angleDiff;

    public float rotationSpeed = 1.5f;
    public float boatSpeed;
    bool rotateTowardsTarget = false;

    private bool _docking = false;
    private bool _docked = false;
    private bool _checkingDock = true;
    private bool _rotateTowardsFinalTarget = false;

    private void Update()
    {
        TargetPosition();
        LookAtTarget();
        boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, 5.0f);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Sail();
        }
        CheckIfDocked();
    }

    private void CheckIfDocked()
    {
        float distToDock = Vector3.Distance(transform.position, new Vector3(95, 0, -100));

        if (distToDock < 1 && _checkingDock)
        {
            _docked = true;
            _rotateTowardsFinalTarget = true;
            _checkingDock = false;
        }
        else if (distToDock > 1)
        {
            _checkingDock = true;
        }
    }

    private void Sail()
    {
        if (_docked == false)
        {

            float distance = Vector3.Distance(transform.position, _targetPosition);
            transform.position = Vector3.Lerp(transform.position, _targetPosition, boatSpeed * Time.deltaTime);
            //boat.AddRelativeForce(Vector3.forward * boatSpeed, ForceMode.Acceleration);
            boatSpeed = 1f;

            //if (distance > 1)
            //{
            //    if (_angleDiff > 135) boatSpeed += 0.01f;
            //    else if (_angleDiff > 90) boatSpeed += 0.015f;
            //    else if (_angleDiff > 45) boatSpeed += 0.02f;
            //    else boatSpeed += 0.025f;
            //}
            //else
            //{
            //    boatSpeed = 0.02f;
            //}
        }
    }

    private void LookAtTarget()
    {
        if (_docked == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
            _angleDiff = Quaternion.Angle(targetRotation, transform.rotation);

            if (rotateTowardsTarget)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                if (_angleDiff < 1) rotateTowardsTarget = false;
            }
        }
        else
        {
            Vector3 finalRotation = new Vector3(95, 0, 0);
            Quaternion finalRotationQ = Quaternion.LookRotation(finalRotation - transform.position);
            _angleDiff = Quaternion.Angle(finalRotationQ, transform.rotation);

            if (_rotateTowardsFinalTarget)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, finalRotationQ, rotationSpeed * Time.deltaTime);
                if (_angleDiff < 1)
                {
                    _rotateTowardsFinalTarget = false;
                    TrashCollector.trashRecycled += TrashCollector.trashCollected;
                    TrashCollector.score += TrashCollector.trashCollected * 10;
                    TrashCollector.trashCollected = 0;
                }
            }     
        }
    }

    private void TargetPosition()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Dock")
                {
                    _docking = true;
                }
                else
                {
                    _docking = false;
                }
            }

            if (!_docking)
            {
                if (XZPlane.Raycast(ray, out distance))
                {
                    // Get clicked position and reset y axis to 0
                    _targetPosition = ray.GetPoint(distance);
                    _targetPosition.y = 0;

                    // Start rotation towards target
                    rotateTowardsTarget = true;
                    //boatSpeed = 0.0f;
                    _docked = false;
                }
            }
            else
            {
                _targetPosition = new Vector3(95, 0, -100);
                rotateTowardsTarget = true;
                boatSpeed = 0.0f;
            }
        }

        //if (Input.GetMouseButtonUp(0))
            //boatSpeed = 0;
    }

}
