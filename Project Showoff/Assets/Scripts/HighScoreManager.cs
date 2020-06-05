using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> TodayNames = new List<GameObject>();
    [SerializeField]
    private List<GameObject> TodayScores = new List<GameObject>();
    [SerializeField]
    private List<GameObject> AllTimeNames = new List<GameObject>();
    [SerializeField]
    private List<GameObject> AllTimeScores = new List<GameObject>();


    private ScoreReader _scoreReader;
    private ScoreManager _scoreManager;

    void Start()
    {
        _scoreManager = new ScoreManager();
        _scoreReader = new ScoreReader();
        _scoreReader.loadHighScore(TodayNames, TodayScores, AllTimeNames, AllTimeScores);
        ScoreManager.Load();

    }
    
}
