using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public EnemyUnitData unitData;

    private void Start()
    {
        GameObject prefab = unitData.EnemyPrefab;
        GameObject clone = Instantiate(prefab, transform.position, quaternion.identity);
        
        clone.GetComponent<MinorEnemy>().SetEnemyData(-3, unitData, transform.position);
    }
}
