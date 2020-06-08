using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndCopyScore : MonoBehaviour
{
    private int _score = 0;
    private string _name = " ";
    private ScoreManager _scoreManager;


    void Awake()
    {
        _score = Manager.Score;
        _name = Manager.Name;

        _scoreManager = new ScoreManager();
        ScoreManager.Load();

        _scoreManager.Add(new Score
        {
            Value = _score,
            PlayerName = _name
        });
        ScoreManager.Save(_scoreManager);
        Debug.LogWarning("Finished !");
    }


    void Update()
    {

    }
}
