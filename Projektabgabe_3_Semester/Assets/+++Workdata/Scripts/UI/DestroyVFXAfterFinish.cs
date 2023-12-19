using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyVFXAfterFinish : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    //get the particle system on the gameobject
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    //destroy the gameobject if the particle system is not playing anymore
    private void Update()
    {
        if (particleSystem.IsAlive())
            return;
        
        Destroy(gameObject);
    }
}
