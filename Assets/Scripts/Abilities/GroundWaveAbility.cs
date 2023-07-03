using Asyncoroutine;
using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWaveAbility : ArtifactAbility
{
    [SerializeField] private float delayBetween = 0.2f;
    [SerializeField] private float distanceBetween = 2.5f;

    [Header("Directional Attack")]
    [SerializeField]
    [Min(0)]
    private int rocksPerAttack = 4;

    [Header("Ring Spawn")]
    [SerializeField] private uint rings = 3;
    [SerializeField] private int rocksPerRing = 8;

    public async void SpawnRocks(Vector3 dir)
    {
        dir = dir.normalized;

        Vector3 startPoint = new Vector3(transform.position.x, 0f , transform.position.z);

        for (int i = 1; i <= rocksPerAttack; i++)
        {
            SpawnSingleRock(startPoint + dir * distanceBetween * i);
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

        Instantiate(attackPrefab, location, Quaternion.LookRotation(dir));
    }

    protected override void UseSpecialAttack()
    {
        base.UseSpecialAttack();

        // Getting direction towards mouse
        Vector3 startingPos = transform.position; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, startingPos); 

        float distance;
        Vector3 endingPos = Vector3.zero;

        if (plane.Raycast(ray, out distance))
        {
            endingPos = ray.GetPoint(distance);
        }

        Vector3 direction = endingPos - startingPos;
        direction.y = 0f;

        SpawnRocks(direction);
    }
}
