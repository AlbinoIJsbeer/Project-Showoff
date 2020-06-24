using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : MonoBehaviour
{
    void Awake()
    {
        Manager.Instance.Name = "";
    }
}
