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
        // TODO: Add a parameter for a direction and normalize it here 
        var dir = Vector3.right + Vector3.back;
        dir = dir.normalized;
        
        for (int i = 1; i <= 4; i++)
        {
            var location = SpawnSingleRock(transform.position + dir * i * 2.5f, dir);
            await new WaitForSeconds(0.2f);
        }
    }

    private Vector3 SpawnSingleRock(Vector3 location, Vector3 dir)
    {
        Instantiate(rockPrefab, location, Quaternion.LookRotation(dir));

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
