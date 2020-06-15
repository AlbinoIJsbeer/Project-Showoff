using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    private float disappearTimer;
    public float disappearTimerMax = 30f;

    private void Awake()
    {
        disappearTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        disappearTimer += Time.deltaTime;

        if (disappearTimer >= disappearTimerMax)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
