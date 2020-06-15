using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCameraFollow : MonoBehaviour
{
    // Target for radar camera to follow
    public Transform boat;
    private Vector3 yAxisOffset;
    private Camera Camera;

    // Reference to boat index
    [SerializeField] private BoatUpgrade boatUpgrade;

    void Start()
    {
        Camera = GetComponent<Camera>();
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        ChangeCameraSize(boatUpgrade.BoatIndex);
        FollowTarget(boat);
    }

    // Radar camera follows the boat
    private void FollowTarget(Transform target)
    {
        transform.position = target.position + yAxisOffset;
    }

    // Change radar camera size based on boat size
    private void ChangeCameraSize(int index)
    {
        if (index == 0)
            Camera.orthographicSize = 175;
        else if (index == 1)
            Camera.orthographicSize = 262.5f;
        else if (index == 2)
            Camera.orthographicSize = 350;
    }
}
