using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrder : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject HighScores;
    [SerializeField]
    private GameObject Settings;
    [SerializeField]
    private GameObject Controls;

    void Start()
    {
        MainMenu.SetActive(true);
        HighScores.SetActive(false);
        Settings.SetActive(false);
        Controls.SetActive(false);
    }

    
}
