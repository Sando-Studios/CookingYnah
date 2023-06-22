using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class Bocchi : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    
    // Start is called before the first frame update
    void  Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}
