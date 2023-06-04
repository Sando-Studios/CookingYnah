using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyUnit", menuName = "Unit/Enemy")]
public class EnemyUnitData : UnitData
{
    [SerializeField] private GameObject drop;
    [SerializeField] private DropItemData data;
    [SerializeField] private float aggroRange;

    public override int MaxHealth { get => base.MaxHealth; set => base.MaxHealth = value; }
    public override int CurrentHealth { get => base.CurrentHealth; set => base.CurrentHealth = value; }
    public override float MoveSpeed { get => base.MoveSpeed; set => base.MoveSpeed = value; }


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
