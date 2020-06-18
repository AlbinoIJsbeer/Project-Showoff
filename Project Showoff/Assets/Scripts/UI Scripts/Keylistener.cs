using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keylistener : MonoBehaviour
{
    [SerializeField]
    private GameObject NextButton;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextButton.SetActive(true);
        }
    }
}
