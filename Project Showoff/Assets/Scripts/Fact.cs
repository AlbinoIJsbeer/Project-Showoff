using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fact : MonoBehaviour
{
    private void OnEnable()
    {
        PauseMenu.GameIsPaused = true;
    }
}
