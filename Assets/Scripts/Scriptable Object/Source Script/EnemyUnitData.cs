using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyUnit", menuName = "Unit/Enemy")]
public class EnemyUnitData : UnitData
{
    [SerializeField] private int unitID = -1;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private DropItemData data;
    [SerializeField] private int basicAttackDmg;

    [Header("Speeds")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackSpeed;

    [Header("Ranges")]
    [SerializeField] private float aggroRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    public int UnitID
    {
        get { return unitID; }
        set { unitID = value; }
    }
    public GameObject Drop
    {
        get { return dropPrefab; }
        set { dropPrefab = value; }
    }
    public DropItemData Data
    {
        get { return data; }
        set { data = value; }
    }

    public int BasicAttackDamage
    {
        get { return basicAttackDmg; }
        set { basicAttackDmg = value; }
    }

    public float AggroRange 
    { 
        get { return aggroRange; } 
        set { aggroRange = value; }
    }
    public float ChaseRange
    {
        get { return chaseRange; }
        set { chaseRange = value; }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public float WanderSpeed
    {
        get { return wanderSpeed; }
        set { wanderSpeed = value; }
    }
    public float ChaseSpeed
    {
        get { return chaseSpeed; }
        set { chaseSpeed = value; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
}
