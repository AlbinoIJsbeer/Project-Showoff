using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampNotification : MonoBehaviour
{
    [SerializeField] private GameObject UIElement;

    void Update()
    {
        Vector3 hitNotPos = Camera.main.WorldToScreenPoint(this.transform.position);
        UIElement.transform.position = Vector3.Lerp(UIElement.transform.position, hitNotPos, 0.5f);
    }
}
