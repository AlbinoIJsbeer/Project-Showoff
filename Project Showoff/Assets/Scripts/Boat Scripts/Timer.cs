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

    void Update()
    {
        TimerCountdown();
    }

    private void TimerCountdown()
    {
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        if (sec == 0)
            timeDisplay.color = new Color(1, 0, 0, 1);
        else
            timeDisplay.color = new Color(1, 1, 1, 1);

        timeDisplay.text = min.ToString("00") + ":" + sec.ToString("00");

        
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
