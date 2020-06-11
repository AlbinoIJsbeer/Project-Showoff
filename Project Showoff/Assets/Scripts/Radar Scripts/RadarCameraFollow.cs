using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCameraFollow : MonoBehaviour
{
    // Target for radar camera to follow
    public Transform target;
    private Vector3 yAxisOffset;
    private Camera camera;

    [SerializeField] private BoatUpgrade boatUpgrade;

    void Start()
    {
        camera = GetComponent<Camera>();
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        // Change radar camera size based on boat size
        if (boatUpgrade.BoatIndex == 0)
            camera.orthographicSize = 175;
        else if (boatUpgrade.BoatIndex == 1)
            camera.orthographicSize = 262.5f;
        else if (boatUpgrade.BoatIndex == 2)
            camera.orthographicSize = 350;

        // Radar camera follows the boat
        transform.position = target.position + yAxisOffset;
    }
}
