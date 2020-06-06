﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 yAxisOffset;

    private void Start()
    {
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    private void LateUpdate()
    {
        if (BoatUpgrade.boatIndex == 0)
            yAxisOffset.y = 90;
        else if (BoatUpgrade.boatIndex == 1)
            yAxisOffset.y = 120;
        else if (BoatUpgrade.boatIndex == 2)
            yAxisOffset.y = 150;

        transform.position = target.position + yAxisOffset;
    }
}
