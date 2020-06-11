using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target for camera to follow
    public Transform target;
    private Vector3 yAxisOffset;

    // Camera offset
    private Vector3 offset;

    // Camera offsets for boat sizes
    private Vector3 smallOffset = new Vector3(0, 50, -60);
    private Vector3 mediumOffset = new Vector3(0, 70, -80);
    private Vector3 largeOffset = new Vector3(0, 90, -100);

    [SerializeField] private BoatUpgrade boatUpgrade;

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        if (BoatController.boatCurrentState == BoatController.BoatState.SAIL)
            SailCamera();
        else if (BoatController.boatCurrentState == BoatController.BoatState.DOCKED)
            DockedCamera();      
    }

    //private void LateUpdate()
    //{
    //    transform.position = target.position + offset;
    //}

    private void DockedCamera()
    {
        transform.position = new Vector3(100, 25, -10);
        transform.eulerAngles = new Vector3(15, 160, 0);
    }

    private void SailCamera()
    {
        transform.eulerAngles = new Vector3(45, 0, 0);

        if (boatUpgrade.BoatIndex == 0)
            offset = Vector3.Lerp(offset, smallOffset, 0.01f);
        else if (boatUpgrade.BoatIndex == 1)
            offset = Vector3.Lerp(offset, mediumOffset, 0.01f);
        else if (boatUpgrade.BoatIndex == 2)
            offset = Vector3.Lerp(offset, largeOffset, 0.01f);

        transform.position = target.position + offset;
    }
}
