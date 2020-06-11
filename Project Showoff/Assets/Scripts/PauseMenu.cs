using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	// This bool will be used for the audiolistener to continue
	public static bool GameIsPaused = false;

	private void Start()
	{
		GameIsPaused = false;
	}

	private void Update()
	{
		if (GameIsPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void Resume()
	{
		GameIsPaused = false;
	}

	public void Pause()
	{
		GameIsPaused = true;
	}
}
