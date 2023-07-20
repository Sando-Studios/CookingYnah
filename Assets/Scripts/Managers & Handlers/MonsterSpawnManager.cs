using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawnManager : MonoBehaviour
{
    [SerializedDictionary("Spawn Point", "Monster Base")]
    [SerializeField] private SerializedDictionary<Transform, Transform> spawnBasePairDictionary = new SerializedDictionary<Transform, Transform>();
    private List<Transform> spawnPointList = new List<Transform>();
    private List<Transform> basePointList = new List<Transform>();
    [SerializedDictionary("Home Base", "Monster Group")] 
    [SerializeField] private SerializedDictionary<Transform, MonsterGroup> baseGroupPairDictionary = new SerializedDictionary<Transform, MonsterGroup>();

    [SerializeField] private GameObject spawnHandlerPrefab;

    private int monsterID = 1;

    public static event Action<int> SpawnNewGroup;

    private void OnEnable()
    {
        SpawningHandler.OnEnemyGroupDeath += SpawnNextWave;

    }
    private void OnDisable()
    {
        SpawningHandler.OnEnemyGroupDeath -= SpawnNextWave;
    }
    private void Start()
    {
        foreach (KeyValuePair<Transform, Transform> pair in spawnBasePairDictionary)
        {
            spawnPointList.Add(pair.Key);
            basePointList.Add(pair.Value);
        }
        SpawnAllMonsters();
    }

    private void SpawnAllMonsters()
    {
        for (int i = 0; i < baseGroupPairDictionary.Count; i++)
        {
            SpawnInitialMonsters(i);
        }
    }

    private void SpawnInitialMonsters(int index)
    {
        GameObject clone = Instantiate(spawnHandlerPrefab, transform);
        SpawningHandler sH = clone.GetComponent<SpawningHandler>();

        Transform spawnPoint = spawnPointList[index];
        Transform basePoint = basePointList[index];

        sH.SetSpawnerData(index, spawnPoint, basePoint);

        Dictionary<EnemyUnitData, int> mGDictionary = baseGroupPairDictionary[basePoint].GroupComposition;

        foreach (var (data, amount) in mGDictionary)
        {
            for (int j = 0; j < amount; j++)
            {
                sH.AddEnemyToSpawn(monsterID++, data);
            }
        }
        sH.StartSpawning();
    }

    public  void SpawnNextWave(int id)
    {
        SpawnNewGroup?.Invoke(id);
    }
}
