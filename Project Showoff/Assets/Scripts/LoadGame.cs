using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log(Manager.Instance.Name);
        if (Manager.Instance.Name != null || Manager.Instance.Name != "")
        {
            SceneManager.LoadScene(1);
        }
    }
}
