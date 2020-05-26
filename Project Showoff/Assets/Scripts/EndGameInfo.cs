using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameInfo : MonoBehaviour
{
    private string name;
    private int score;

    public TMP_Text nameDisplay;
    public TMP_Text scoreDisplay;

    void Start()
    {
        name = Manager.Instance.name;
        score = Manager.Instance.score;
    }

    private void Update()
    {
        nameDisplay.text = name;
        scoreDisplay.text = score.ToString();
    }
}
