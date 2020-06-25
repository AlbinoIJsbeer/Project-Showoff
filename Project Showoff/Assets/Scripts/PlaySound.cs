using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private string sound;

    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play(sound);
    }
}
