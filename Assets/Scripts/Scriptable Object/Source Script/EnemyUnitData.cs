using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyUnit", menuName = "Unit/Enemy")]
public class EnemyUnitData : UnitData
{
    [SerializeField] private GameObject drop;
    [SerializeField] private DropItemData data;
    [SerializeField] private float aggroRange;

    public GameObject Drop
    {
        get { return drop; }
        set { drop = value; }
    }
    public DropItemData Data
    {
        get { return data; }
        set { data = value; }
    }
    public float AggroRange 
    { 
        get { return aggroRange; } 
        set { aggroRange = value; }
    }
}
