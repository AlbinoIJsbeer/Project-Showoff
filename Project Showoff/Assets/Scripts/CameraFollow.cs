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

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        // REMOVE STATIC VARs
        if (BoatUpgrade.boatIndex == 0)
            offset = Vector3.Lerp(offset, smallOffset, 0.01f);
        else if (BoatUpgrade.boatIndex == 1)
            offset = Vector3.Lerp(offset, mediumOffset, 0.01f);
        else if (BoatUpgrade.boatIndex == 2)
            offset = Vector3.Lerp(offset, largeOffset, 0.01f);
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
