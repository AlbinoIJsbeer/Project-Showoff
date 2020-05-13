using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    GameObject trash;

    private void Start()
    {
        trash = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Net")
        {
            Debug.Log("Collision");
            Destroy(trash);
        }
    }
}
