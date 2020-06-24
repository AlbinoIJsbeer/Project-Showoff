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
        // Pings trash on radar
        if (other.tag == "Trash")
        {
            // If the object has been already detected within half rotation of the sweep then skip
            if (!colliders.Contains(other))
            {
                RadarPing ping = Instantiate(radarPing, other.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                ping.transform.localScale = new Vector3(5, 5, 5);
                ping.transform.parent = radarPings.transform;
                colliders.Add(other);
            }
        }

        // Pings dock on radar
        if (other.tag == "Dock")
        {
            // If the object has been already detected within half rotation of the sweep then skip
            if (!colliders.Contains(other))
            {
                RadarPing ping = Instantiate(radarPing, new Vector3(other.transform.position.x, 0, other.transform.position.z), Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                ping.SetColor(new Color(0, 1, 1, 1));
                ping.transform.localScale = new Vector3(25, 25, 25);
                ping.transform.parent = radarPings.transform;
                colliders.Add(other);
            }    
        }

        // Pings animal on radar
        if (other.tag == "Animal")
        {
            if (!colliders.Contains(other))
            {
                RadarPing ping = Instantiate(radarPing, new Vector3(other.transform.position.x, 0, other.transform.position.z), Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                ping.SetColor(new Color(1, 1, 0, 1));
                ping.transform.localScale = new Vector3(30, 30, 30);
                ping.transform.parent = radarPings.transform;
                colliders.Add(other);
            }
        }

        if (other.tag == "Obstacle")
        {
            if (!colliders.Contains(other))
            {
                RadarPing ping = Instantiate(radarPing, new Vector3(other.transform.position.x, 0, other.transform.position.z), Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                ping.SetColor(new Color(1, 0, 0, 1));
                ping.transform.localScale = new Vector3(15, 15, 15);
                ping.transform.parent = radarPings.transform;
                colliders.Add(other);
            }
        }
    }
}
