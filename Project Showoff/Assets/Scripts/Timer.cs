using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public float timer = 120.00f;

    void Update()
    {
        TimerCountdown();
    }

    private void TimerCountdown()
    {
        timeDisplay.text = timer.ToString("F2");
        timer -= Time.deltaTime;
        if (timer <= 0)
            SceneManager.LoadScene(2);
    }
}
