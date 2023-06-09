using Asyncoroutine;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningHandler : MonoBehaviour
{
    private GameObject monsterPrefab;
    private Dictionary<int, EnemyUnitData> enemiesToSpawn = new Dictionary<int, EnemyUnitData>();
    private Transform spawnPoint;
    private Transform homeBase;

    private List<Enemy> enemiesSpawned = new List<Enemy>();

    public void SetData(GameObject prefab, Transform monsterSpawnPoint, Transform monsterHomeBase)
    {
        monsterPrefab = prefab;
        spawnPoint = monsterSpawnPoint;
        homeBase = monsterHomeBase;
    }

    public void AddEnemyToSpawn(int id, EnemyUnitData unitData)
    {
        enemiesToSpawn.Add(id, unitData);
    }

    public void StartSpawning()
    {
        Spawning(homeBase.position);
    }

    private async void Spawning(Vector3 home)
    {
        foreach (KeyValuePair<int, EnemyUnitData> e in enemiesToSpawn)
        {
            GameObject clone = Instantiate(monsterPrefab, spawnPoint);

            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.SetData(e.Key, e.Value, home);
            enemiesSpawned.Add(enemy);
            await new WaitForSeconds(0.5f);
        }
    }

    private async void BatchDelay()
    {
        await new WaitForSeconds(4.0f);

    }
}
