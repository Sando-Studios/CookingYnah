using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using Random = UnityEngine.Random;

public class FireLightHandler : MonoBehaviour
{
    [SerializeField] private Light lightSource;
    // Start is called before the first frame update
    void Start()
    {
        Flicker();
    }

    private async void Flicker()
    {
        for (;;)
        {
            if(this == null)
                return;
            
            float intensity = Random.Range(0.5f, 1.3f);
            lightSource.intensity = intensity;
            await new WaitForSeconds(0.1f);
        }
        
    }
}
