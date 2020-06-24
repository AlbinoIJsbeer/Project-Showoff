using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    ParticleSystem dust;

    private void Start()
    {
        dust = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!dust.IsAlive())
            Destroy(this.gameObject);
    }
}
