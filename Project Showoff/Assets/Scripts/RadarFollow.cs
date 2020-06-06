using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 yAxisOffset;

    private Vector3 scaleChange;

    void Start()
    {
        scaleChange = new Vector3(1, 1, 1);
        yAxisOffset = new Vector3(0, this.transform.position.y, 0);
    }

    void LateUpdate()
    {
        transform.position = target.position + yAxisOffset;

        if (BoatUpgrade.boatIndex == 0)
            scaleChange = new Vector3(1, 1, 1);
        else if (BoatUpgrade.boatIndex == 1)
            scaleChange = new Vector3(1.5f, 1, 1.5f);
        else if (BoatUpgrade.boatIndex == 2)
            scaleChange = new Vector3(2, 1, 2);

        transform.localScale = scaleChange;
    }
}
