using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableNextOnNameEnter : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button next;
    private string text;

    void Update()
    {
        text = input.text;

        if (text.Length == 0)
            next.interactable = false;
        else
            next.interactable = true;
    }
}
