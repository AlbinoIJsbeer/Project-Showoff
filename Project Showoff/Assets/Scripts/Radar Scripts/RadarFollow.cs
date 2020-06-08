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

    void Start()
    {
        scaleChange = new Vector3(1, 1, 1);
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        transform.position = target.position + yAxisOffset;

        // Change radar scaler based on boat size
        if (BoatUpgrade.boatIndex == 0)
            scaleChange = new Vector3(1, 1, 1);
        else if (BoatUpgrade.boatIndex == 1)
            scaleChange = new Vector3(1.5f, 1, 1.5f);
        else if (BoatUpgrade.boatIndex == 2)
            scaleChange = new Vector3(2, 1, 2);

        // Scale radar size
        transform.localScale = scaleChange;
    }
}
