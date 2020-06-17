using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePopUpPosition : MonoBehaviour
{
    [SerializeField] private Transform popUpPos;
    private BoatUpgrade boatUpgrade;

    private float y;

    void Start()
    {
        boatUpgrade = GetComponent<BoatUpgrade>();
        y = 15;
    }

    void Update()
    {
        ChangeYAxisValue(boatUpgrade.BoatIndex);
        popUpPos.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void ChangeYAxisValue(int index)
    {
        if (index == 0)
            y = 15;
        else if (index == 1)
            y = 20;
        else if (index == 2)
            y = 30;
    }
}
