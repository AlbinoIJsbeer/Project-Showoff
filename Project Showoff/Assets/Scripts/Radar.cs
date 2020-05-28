using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform sweepTransform;
    private float rotationSpeed;

    private void Awake()
    {
        sweepTransform = transform.Find("Sweep");
        rotationSpeed = 90f;
    }

    void Update()
    {
        // Check if the sweep has made half of a rotation
        float previousRotation = (sweepTransform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        float currentRotation = (sweepTransform.eulerAngles.y % 360) - 180;

        // If sweep made half of a rotation then clear the colliders list
        // This is done due to the reason that some objects might pop up twice
        // depending on the movement of the Boat (Rardar as well). This prevents it.
        if (previousRotation < 0 && currentRotation >= 0)
        {
            SweepCollider.colliders.Clear();
        }
    }
}