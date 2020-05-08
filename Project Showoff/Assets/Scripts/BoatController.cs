using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody player;
    GameObject net;

    public float maxVelocity = 10.0f;
    public float acceleration = 1.1f;
    public float decceleration = 0.9f;

    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);
    Vector3 targetPosition;

    public float testForce = 10.0f;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        //targetPosition = player.transform.position;
        net = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        //Sail();
        TargetPosition();
        

    }


    private void FixedUpdate()
    {
        Vector3 direction = (targetPosition - player.transform.position).normalized;
        player.MovePosition(player.transform.position + direction * acceleration * Time.deltaTime);
        //Sail();
    }

    private void Sail()
    {
        float distance = Vector3.Distance(player.transform.position, targetPosition);
        player.MovePosition(targetPosition);
        //if (distance > 10)
        //{
        //    player.transform.LookAt(hitPoint);
        //    //player.velocity += transform.forward * (acceleration);
        //    //player.AddRelativeForce(Vector3.forward * acceleration);
        //    player.MovePosition(hitPoint);
        //}
        //else if (distance < 10)
        //{
        //    //player.velocity -= transform.forward * (acceleration);
        //    player.AddRelativeForce(Vector3.forward * -acceleration);
        //}
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
            }
        }
    }
}
