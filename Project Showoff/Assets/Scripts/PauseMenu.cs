using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	// This bool will be used for the audiolistener to continue
	public static bool GameIsPaused = false;

	private void Start()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}
