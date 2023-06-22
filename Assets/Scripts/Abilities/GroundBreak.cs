using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    
    public async void SpawnRocks()
    {
        for (int i = 0; i < 4; i++)
        {
            var location = SpawnSingleRock(transform.position + Vector3.right * i * 2.5f);
            await new WaitForSeconds(0.2f);
        }
    }

    private Vector3 SpawnSingleRock(Vector3 location)
    {
        Instantiate(rockPrefab, location, Quaternion.identity);
        
        
        return location + Vector3.right;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnRocks();
        }
    }
}
