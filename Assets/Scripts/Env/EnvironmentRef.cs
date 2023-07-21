using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class EnvironmentRef : MonoBehaviour
{
    public static EnvironmentRef Instance;

    public SerializedDictionary<string, Collider> Colliders;

    public SerializedDictionary<string, GameObject> objects;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
