using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : MonoBehaviour
{

    public void SetPlayerName(string _name)
    {
        Manager.Instance.Name = _name;
    }
}
