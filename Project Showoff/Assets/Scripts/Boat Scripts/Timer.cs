using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public float timer = 120.00f;
    public ScoreManager scoreManager;

    void Start()
    {
        //scoreManager = new ScoreManager();
        //ScoreManager.Load();
    }

    void Update()
    {
        TimerCountdown();
    }

    private void TimerCountdown()
    {
        timeDisplay.text = timer.ToString("F2");
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            //scoreManager.Add(new Score
            //{
            //    Value = TrashCollector.score,
            //    PlayerName = "Test"
            //});
            //ScoreManager.Save(scoreManager);
            SceneManager.LoadScene(2);
        }

    }
}
