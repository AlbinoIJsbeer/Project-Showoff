using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    private float timer;

    void Start()
    {
        timer = 30;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            timer = 30;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
            SceneManager.LoadScene(0);
    }
}
