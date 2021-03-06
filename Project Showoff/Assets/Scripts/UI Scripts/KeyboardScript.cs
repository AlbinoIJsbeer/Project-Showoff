﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardScript : MonoBehaviour
{

    private string _word = null;
    private int _wordIndex = 0;
    private string alpha;
    [SerializeField]
    private TMP_InputField _name = null;

    public void alphabetFunction(string _alphabet)
    {
        _wordIndex++;
        _word = _word + _alphabet;
        _name.text = _word;
    }

    public void BackSpace()
    {
        if (_wordIndex > 0)
        {
            _wordIndex--;
            _name.text = _name.text.Substring(0, _wordIndex);
            Manager.Instance.Name = _name.text.Substring(0, _wordIndex);
        }
        else
        {
            _name.text = "";
            Manager.Instance.Name = "";
        }

    }
}
