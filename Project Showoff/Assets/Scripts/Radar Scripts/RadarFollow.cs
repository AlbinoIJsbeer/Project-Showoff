using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarFollow : MonoBehaviour
{
    // Target for radar to follow
    public Transform target;
    private Vector3 yAxisOffset;

    // Radar scaler
    private Vector3 scaleChange;

    // Reference to boat index
    [SerializeField] private BoatUpgrade boatUpgrade;

    void Start()
    {
        // Starting scale and 'y' axis offset
        scaleChange = new Vector3(1, 1, 1);
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        FollowTarget();
        ChangeRadarScale();
    }

    // Follow the boat
    private void FollowTarget()
    {
        transform.position = target.position + yAxisOffset;
    }

    // Change radar size based on boat 
    private void ChangeRadarScale()
    {
        if (boatUpgrade.BoatIndex == 0)
            scaleChange = new Vector3(1, 1, 1);
        else if (boatUpgrade.BoatIndex == 1)
            scaleChange = new Vector3(1.5f, 1, 1.5f);
        else if (boatUpgrade.BoatIndex == 2)
            scaleChange = new Vector3(2, 1, 2);

        transform.localScale = scaleChange;
    }
}
