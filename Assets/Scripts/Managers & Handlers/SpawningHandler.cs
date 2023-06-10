using Asyncoroutine;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningHandler : MonoBehaviour
{
    private int spawnerID;
    private GameObject monsterPrefab;
    private Dictionary<int, EnemyUnitData> enemiesToSpawn = new Dictionary<int, EnemyUnitData>();
    private Transform spawnPoint;
    private Vector3 homeBase;

    private List<Enemy> enemiesSpawned = new List<Enemy>();

    public static event Action<int> OnEnemyGroupDeath;

    private void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += OnMemberDeath;
        MonsterSpawnManager.SpawnNewGroup += SpawnGroup;
    }
    private void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= OnMemberDeath;
        MonsterSpawnManager.SpawnNewGroup -= SpawnGroup;
    }

    public void SetData(int id, GameObject prefab, Transform monsterSpawnPoint, Transform monsterHomeBase)
    {
        spawnerID = id;
        monsterPrefab = prefab;
        spawnPoint = monsterSpawnPoint;
        homeBase = monsterHomeBase.position;
    }

    public void AddEnemyToSpawn(int id, EnemyUnitData unitData)
    {
        enemiesToSpawn.Add(id, unitData);
    }

    public void StartSpawning()
    {
        Spawning(homeBase);
    }
    private void SpawnGroup(int i)
    {
        if (spawnerID == i)
        {
            BatchDelay();
        }
    }

    private void Update()
    {
        int w = 1;
        if (Input.anyKeyDown) //for testing spawning waves
        {
            Debug.Log("KEY SPAWN");
            
            if (spawnerID == w)
                enemiesSpawned.Clear();
            OnMemberDeath(-1);
        }
    }

    private void OnMemberDeath(int i)
    {
        enemiesSpawned.RemoveAll(enemy => enemy.GetEnemyUnitData().UnitID == i);

        if (enemiesSpawned.Count == 0)
        {
            OnEnemyGroupDeath?.Invoke(spawnerID);
        }
    }

    private async void Spawning(Vector3 home)
    {
        foreach (KeyValuePair<int, EnemyUnitData> e in enemiesToSpawn)
        {
            GameObject clone = Instantiate(monsterPrefab, spawnPoint);

            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.SetData(e.Key, e.Value, home);
            enemiesSpawned.Add(enemy);
            await new WaitForSeconds(0.7f);
        }
    }

    private async void BatchDelay()
    {
        await new WaitForSeconds(4.0f);
        StartSpawning();
    }
}
