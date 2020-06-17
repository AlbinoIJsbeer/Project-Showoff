using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampNotification : MonoBehaviour
{
    [SerializeField] private GameObject hitNotification;

    void Update()
    {
        Vector3 hitNotPos = Camera.main.WorldToScreenPoint(this.transform.position);
        hitNotification.transform.position = Vector3.Lerp(hitNotification.transform.position, hitNotPos, 0.5f);
    }
}
