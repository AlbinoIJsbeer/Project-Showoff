using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPref : MonoBehaviour
{
    public static void SaveAll()
    {
        PlayerPrefs.SetFloat(Manager.Instance.Name + " Score:", Manager.Instance.Score);
        PlayerPrefs.Save();
    }
}
