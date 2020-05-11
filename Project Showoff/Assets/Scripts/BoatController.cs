using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody player;

    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);
    private Vector3 targetPosition;
    private float angleDiff;

    public float rotationSpeed = 3;
    public float boatSpeed = 0;
    public float boatMaxSpeed = 5;

    bool rotateTowardsTarget = false;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TargetPosition();
        LookAtTarget();
    }

    private void FixedUpdate()
    {
        Sail();
    }

    private void Sail()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, boatSpeed * Time.deltaTime);
        //player.AddRelativeForce(Vector3.forward * boatSpeed);
        if (boatSpeed >= boatMaxSpeed) boatSpeed = boatMaxSpeed;

        if (boatSpeed < boatMaxSpeed && rotateTowardsTarget)
        {
            if (angleDiff > 135) boatSpeed += 0.01f;
            else if (angleDiff > 90) boatSpeed += 0.02f;
            else if (angleDiff > 45) boatSpeed += 0.03f;
            else boatSpeed += 0.04f;
        }
        else boatSpeed = 0;

    }

    private void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        angleDiff = Quaternion.Angle(targetRotation, transform.rotation);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(angleDiff);
        }

        if (rotateTowardsTarget)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (angleDiff < 1) rotateTowardsTarget = false; 
        }
    }

    private void TargetPosition()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (XZPlane.Raycast(ray, out distance))
            {
                targetPosition = ray.GetPoint(distance);
                targetPosition.y = 0;
                rotateTowardsTarget = true;
            }
        }
    }
}
