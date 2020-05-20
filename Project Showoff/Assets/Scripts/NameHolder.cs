using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameHolder : MonoBehaviour
{
    private string name;
    public TMP_Text getName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        name = getName.text;
        Manager.Instance.name = name;
    }
}
