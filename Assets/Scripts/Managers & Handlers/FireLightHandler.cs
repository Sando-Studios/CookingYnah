using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

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
        while (gameObject.activeInHierarchy)
        {
            float intensity = Random.Range(0.5f, 1.3f);
            lightSource.intensity = intensity;
            await new WaitForSeconds(0.1f);
        }
        
    }
}
