using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeDisplay;
    public float timer = 120.00f;
    public ScoreManager scoreManager;

    void Start()
    {
        scoreManager = new ScoreManager();
    }

    void Update()
    {
        TimerCountdown();

        if (Input.GetKeyDown(KeyCode.G))
        {
            timer = 3;
        }
    }

    private void TimerCountdown()
    {
        // Minutes and Seconds
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        // Change time text color
        if (timer > 5)
        {
            // If a full minute(s) is left then highlight time in red
            if (sec == 0)
                timeDisplay.color = new Color(1, 0, 0, 1);
            else
                timeDisplay.color = new Color(1, 1, 1, 1);
        }
        else
        {
            // If 5 seconds are left then highlight time in red
            timeDisplay.color = new Color(1, 0, 0, 1);
        }

        // Display time in "mm:ss" format
        timeDisplay.text = min.ToString("00") + ":" + sec.ToString("00");
        
        timer -= Time.deltaTime;

        if (timer <= 3)
            FindObjectOfType<AudioManager>().Play("TimeTicking");

        // If there is no time left, then go to end screen
        if (timer <= 0)
        {
            HighScoreTable.AddHighScoreEntry(Manager.Instance.Score, Manager.Instance.Name);
            SceneManager.LoadScene(2);
        }
    }

    public void DeductTime(float seconds)
    {
        timer -= seconds;
    }
}
