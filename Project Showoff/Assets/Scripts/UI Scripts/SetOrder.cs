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
    [SerializeField]
    private GameObject Storyline;


    [SerializeField]
    private GameObject NameField;
    [SerializeField]
    private GameObject Storyline1;
    [SerializeField]
    private GameObject Storyline2;
    [SerializeField]
    private GameObject Storyline3;
    [SerializeField]
    private GameObject Storyline4;
    [SerializeField]
    private GameObject Storyline5;
    void Start()
    {
        MainMenu.SetActive(true);
        HighScores.SetActive(false);
        Settings.SetActive(false);
        Controls.SetActive(false);
        

            NameField.SetActive(true);
            Storyline.SetActive(false);
            Storyline1.SetActive(false);
            Storyline2.SetActive(false);
            Storyline3.SetActive(false);
            Storyline4.SetActive(false);
            Storyline5.SetActive(false);
    }

    
}
