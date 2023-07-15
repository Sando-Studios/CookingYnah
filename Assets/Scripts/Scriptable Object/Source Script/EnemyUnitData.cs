using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyUnit", menuName = "Unit/Enemy")]
public class EnemyUnitData : UnitData
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int unitID = -1;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private SerializedDictionary<ItemData, float> dropData = new SerializedDictionary<ItemData, float>();
    [SerializeField] private int basicAttackDmg;

    [Header("Speeds")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackSpeed;

    [Header("Ranges")]
    [SerializeField] private float aggroRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    [Header("Animations")]
    [SerializeField] private RuntimeAnimatorController animatorController;


    [Header("Enemy Prefab")] 
    [SerializeField] private GameObject enemyPrefab;

    public void Init(int id, EnemyUnitData unitDataSource)
    {
        UnitID = id;
        UnitName = unitDataSource.UnitName;
        MaxHealth = unitDataSource.MaxHealth;
        CurrentHealth = unitDataSource.MaxHealth;
        DropObject = unitDataSource.DropObject;
        DropData = unitDataSource.DropData;
        AggroRange = unitDataSource.AggroRange;
        BasicAttackDamage = unitDataSource.BasicAttackDamage;
        ChaseRange = unitDataSource.ChaseRange;
        AttackRange = unitDataSource.AttackRange;
        PatrolSpeed =   unitDataSource.PatrolSpeed;
        ChaseSpeed = unitDataSource.ChaseSpeed;
        AttackSpeed = unitDataSource.AttackSpeed;
        Controller = unitDataSource.Controller;
        EnemyPrefab = unitDataSource.EnemyPrefab;
    }



    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int UnitID
    {
        get { return unitID; }
        set { unitID = value; }
    }
    public GameObject DropObject
    {
        get { return dropPrefab; }
        set { dropPrefab = value; }
    }
    public SerializedDictionary<ItemData, float> DropData
    {
        get { return dropData; }
        set { dropData = value; }
    }

    public RuntimeAnimatorController Controller 
    { 
        get { return animatorController; } 
        set { animatorController = value; }
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

    public float PatrolSpeed
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

    public GameObject EnemyPrefab
    {
        get => enemyPrefab;
        set { enemyPrefab = value; }
    }
}
