using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepCollider : MonoBehaviour
{
    // Parent object, which is a child of the RadarCamera
    [SerializeField] private Transform radarPings;

    // Radar ping prefab
    [SerializeField] private Transform radarPing;

    // Keep track of colliders hit by the sweep
    // This prevents an objects showing twice on the radar
    public static List<Collider> colliders;

    private void Awake()
    {
        colliders = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            // If the object has been already detected within half rotation of the sweep then skip
            if (!colliders.Contains(other))
            {
                var ping = Instantiate(radarPing, other.transform.position, Quaternion.Euler(90, 0, 0));
                ping.transform.parent = radarPings.transform;
                colliders.Add(other);
            }
        }
    }
}
