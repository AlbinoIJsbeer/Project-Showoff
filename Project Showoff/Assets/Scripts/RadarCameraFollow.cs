using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 yAxisOffset;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        if (BoatUpgrade.boatIndex == 0)
            camera.orthographicSize = 175;
        else if (BoatUpgrade.boatIndex == 1)
            camera.orthographicSize = 262.5f;
        else if (BoatUpgrade.boatIndex == 2)
            camera.orthographicSize = 350;

        transform.position = target.position + yAxisOffset;
    }
}
