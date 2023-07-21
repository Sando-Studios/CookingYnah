using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    public static BGMHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
