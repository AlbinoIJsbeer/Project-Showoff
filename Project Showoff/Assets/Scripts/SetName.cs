using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : MonoBehaviour
{
    [SerializeField] private GameObject manager;

    void Start()
    {
        manager.name = "";
    }
}
