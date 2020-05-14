using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BoatController : MonoBehaviour
{
    public Rigidbody player;

    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);
    private Vector3 targetPosition;
    private float angleDiff;

    [Range(0, 3)]
    public float rotationSpeed = 1.5f;
    [Range(0, 5)]
    public float boatSpeed = 0f;
    bool rotateTowardsTarget = false;

    bool docking = false;
    bool docked = false;
    bool checkingDock = true;
    bool rotateTowardsFinalTarget = false;

    public TMP_Text trashDisplay;
    private int trashCollected = 0;
    public TMP_Text trashRecycledDisplay;
    private int trashRecycled = 0;
    public TMP_Text scoreDisplay;
    private int score = 0;
    public TMP_Text timeDisplay;
    private float timer = 120.00f;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TargetPosition();
        LookAtTarget();
        trashDisplay.text = trashCollected.ToString();
        trashRecycledDisplay.text = trashRecycled.ToString();
        scoreDisplay.text = score.ToString();
        timeDisplay.text = timer.ToString("F2");
        boatSpeed = Mathf.Clamp(boatSpeed, 0.0f, 5.0f);

        Manager.Instance.score = score;
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        Sail();
        CheckIfDocked();
    }

    private void CheckIfDocked()
    {
        float distToDock = Vector3.Distance(transform.position, new Vector3(95, 0, -100));

        if (distToDock < 1 && checkingDock)
        {
            docked = true;
            rotateTowardsFinalTarget = true;
            checkingDock = false;
        }
        else if (distToDock > 1)
        {
            checkingDock = true;
        }
    }

    private void Sail()
    {
        if (docked == false)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, boatSpeed * Time.deltaTime);

            if (distance > 1)
            {
                if (angleDiff > 135) boatSpeed += 0.01f;
                else if (angleDiff > 90) boatSpeed += 0.015f;
                else if (angleDiff > 45) boatSpeed += 0.02f;
                else boatSpeed += 0.025f;
            }
            else
            {
                boatSpeed = 0.0f;
            }
        }
    }

    private void LookAtTarget()
    {
        if (docked == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            angleDiff = Quaternion.Angle(targetRotation, transform.rotation);

            if (rotateTowardsTarget)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                if (angleDiff < 1) rotateTowardsTarget = false;
            }
        }
        else
        {
            Debug.Log("Rotation in progress");
            Vector3 finalRotation = new Vector3(95, 0, 0);
            Quaternion finalRotationQ = Quaternion.LookRotation(finalRotation - transform.position);
            angleDiff = Quaternion.Angle(finalRotationQ, transform.rotation);

            if (rotateTowardsFinalTarget)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, finalRotationQ, rotationSpeed * Time.deltaTime);
                if (angleDiff < 1) rotateTowardsFinalTarget = false; trashRecycled += trashCollected; score += trashCollected * 10; trashCollected = 0;
            }     
        }
    }

    private void TargetPosition()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Dock")
                {
                    docking = true;
                }
                else
                {
                    docking = false;
                }
            }

            if (!docking)
            {
                if (XZPlane.Raycast(ray, out distance))
                {
                    // Get clicked position and reset y axis to 0
                    targetPosition = ray.GetPoint(distance);
                    targetPosition.y = 0;

                    // Start rotation towards target
                    rotateTowardsTarget = true;
                    boatSpeed = 0.1f;
                    docked = false;
                }
            }
            else
            {
                targetPosition = new Vector3(95, 0, -100);
                rotateTowardsTarget = true;
                boatSpeed = 0.1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            if (trashCollected <= 50)
            {
                Debug.Log("Collied with Trash");
                Destroy(other.gameObject);
                trashCollected++;
                score++;
            }
        }
    }

}
