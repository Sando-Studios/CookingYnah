using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject rockPrefab;

    [SerializeField] private float delayBetween = 0.2f;
    [SerializeField] private float distanceBetween = 2.5f;

    [Header("Ring Spawn")]
    [SerializeField] private uint rings = 3;
    [SerializeField] private int rocksPerRing = 8;

    public async void SpawnRocks(Vector3 dir)
    {
        dir = dir.normalized;
        
        for (int i = 1; i <= 4; i++)
        {
            SpawnSingleRock(transform.position + dir * distanceBetween * i);
            await new WaitForSeconds(delayBetween);
        }
    }

    public async void SpawnRocks(uint rings)
    {
        void SpawnRing(float dist)
        {
            var increment = 360 / rocksPerRing;
            
            for (int i = 0; i < 360; i += increment)
            {
                var rad = Mathf.Deg2Rad * i;

                var y = Mathf.Cos(rad);
                var x = Mathf.Sin(rad);
                
                var loc = transform.position + new Vector3(x, 0, y) * dist;
                
                SpawnSingleRock(loc);
            }
        }
        
        for (uint i = 1; i <= rings; i++)
        {
            SpawnRing(i * distanceBetween);
            await new WaitForSeconds(delayBetween);
        }
    }

    private void SpawnSingleRock(Vector3 location)
    {
        var dir = location - transform.position;
        dir = dir.normalized;
        
        Instantiate(rockPrefab, location, Quaternion.LookRotation(dir));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // SpawnRocks(Vector3.right + Vector3.forward);
            SpawnRocks(rings);
        }
        
    }
}
