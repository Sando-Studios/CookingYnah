using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterGroup", menuName = "DataAsset/Monster Group")]
public class MonsterGroup : ScriptableObject
{
    [SerializeField] private SerializedDictionary<EnemyUnitData, int> groupComposition = new SerializedDictionary<EnemyUnitData, int>();

    public SerializedDictionary<EnemyUnitData, int> GroupComposition
    {
        get { return groupComposition; }
    }
}
