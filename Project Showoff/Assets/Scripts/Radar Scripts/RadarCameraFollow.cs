using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCameraFollow : MonoBehaviour
{
    // Target for radar camera to follow
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
        // Change radar camera size based on boat size
        if (BoatUpgrade.boatIndex == 0)
            camera.orthographicSize = 175;
        else if (BoatUpgrade.boatIndex == 1)
            camera.orthographicSize = 262.5f;
        else if (BoatUpgrade.boatIndex == 2)
            camera.orthographicSize = 350;

        // Radar camera follows the boat
        transform.position = target.position + yAxisOffset;
    }
}
