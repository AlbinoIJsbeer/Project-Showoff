using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    private float disappearTimer;
    public float disappearTimerMax = 3f;

    private void OnEnable()
    {
        disappearTimer = 0;
    }

    void Update()
    {
        disappearTimer += Time.deltaTime;

        if (disappearTimer >= disappearTimerMax)
            gameObject.SetActive(false);
    }
}
