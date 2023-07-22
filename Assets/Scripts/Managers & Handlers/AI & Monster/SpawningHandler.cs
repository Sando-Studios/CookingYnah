using Asyncoroutine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawningHandler : MonoBehaviour
{
    private int spawnerID;
    private Dictionary<int, EnemyUnitData> enemiesToSpawn = new Dictionary<int, EnemyUnitData>();
    private Transform spawnPoint;
    private Vector3 homeBase;

    private List<MinorEnemy> enemiesSpawned = new List<MinorEnemy>();

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

    public void SetSpawnerData(int id, Transform monsterSpawnPoint, Transform monsterHomeBase)
    {
        spawnerID = id;
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
        /*int w = 1;
        if (Input.anyKeyDown) //for testing spawning waves
        {
            Debug.Log("KEY SPAWN");
            
            if (spawnerID == w)
                enemiesSpawned.Clear();
            OnMemberDeath(-1);
        }*/
    }

    private void OnMemberDeath(int i)
    {
        enemiesSpawned.RemoveAll(enemy => enemy.GetEnemyData().UnitID == i);

        if (enemiesSpawned.Count == 0)
        {
            OnEnemyGroupDeath?.Invoke(spawnerID);
        }
    }

    private async void Spawning(Vector3 home)
    {
        foreach (KeyValuePair<int, EnemyUnitData> e in enemiesToSpawn)
        {
            GameObject clone = Instantiate(e.Value.EnemyPrefab, spawnPoint);

            MinorEnemy enemy = clone.GetComponent<MinorEnemy>();
            enemy.SetEnemyData(e.Key, e.Value, home);
            enemiesSpawned.Add(enemy);
            await new WaitForSeconds(0.7f);
        }
    }

    private async void BatchDelay()
    {
        await new WaitForSeconds(4.0f);

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Dungeon Level")
            StartSpawning();
    }
}
