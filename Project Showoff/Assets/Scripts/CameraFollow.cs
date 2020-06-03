using System.Collections;
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
        transform.position = target.position + yAxisOffset;
    }
}
