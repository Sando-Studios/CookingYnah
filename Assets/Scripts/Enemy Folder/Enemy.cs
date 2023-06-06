using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Unit Data")]
    [SerializeField] private EnemyUnitData enemyUnitData;

    private EnemyUnitData enemyDataInstance;
    [SerializeField] private SphereCollider aggroTrigger;
    [SerializeField] private GameObject targetUnit;
    [SerializeField] private Vector3 home;

    [Header("SFX")]
    public Material redMaterial;
    public Material greenMaterial;
    

    [Header("Health UI")]
    public Image hpBar;

    private void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += Death;
    }
    private void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= Death;
    }

    // Start is called before the first frame update
    void Start()
    {
        // enemyDataInstance = new EnemyUnitData();

        SetData(10001); // To be called and set by spawner 

        MonsterStateManager.Instance.AddMonster(this, new IdleState(MonsterStateManager.Instance, this));
    }

    public void SetData(int enemyID)
    {
        enemyDataInstance.UnitID = enemyID;
        enemyDataInstance.MaxHealth = enemyUnitData.MaxHealth;
        enemyDataInstance.CurrentHealth = enemyDataInstance.MaxHealth;
        enemyDataInstance.UnitName = enemyUnitData.UnitName;
        enemyDataInstance.MoveSpeed = enemyUnitData.MoveSpeed;
        enemyDataInstance.Drop = enemyUnitData.Drop;
        enemyDataInstance.Data = enemyUnitData.Data;
        enemyDataInstance.AggroRange = enemyUnitData.AggroRange;
        enemyDataInstance.BasicAttackDamage = enemyUnitData.BasicAttackDamage;
        enemyDataInstance.ChaseRange = enemyUnitData.ChaseRange;
        enemyDataInstance.AttackRange = enemyUnitData.AttackRange;
        enemyDataInstance.PatrolSpeed = enemyUnitData.PatrolSpeed;
        enemyDataInstance.ChaseSpeed = enemyUnitData.ChaseSpeed;
        enemyDataInstance.AttackSpeed = enemyUnitData.AttackSpeed;

        aggroTrigger.radius = enemyDataInstance.AggroRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            targetUnit = other.gameObject;
            aggroTrigger.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float a = enemyDataInstance.CurrentHealth;
        float b = enemyDataInstance.MaxHealth;
        float normalized = a / b;

        hpBar.fillAmount = normalized;
    }

    public void ResetAggro()
    {
        targetUnit = null;
        aggroTrigger.enabled = true;
    }

    public Vector3 GetHome()
    {
        return home;
    }
    public GameObject GetTargetUnit()
    {
        return targetUnit;
    }
    public EnemyUnitData GetEnemyUnitData()
    {
        return enemyDataInstance;
    }

    public async void Hit()// To be replaced by animations 
    {
        GetComponentInChildren<Renderer>().material = redMaterial;
        await new WaitForSeconds(0.5f);
        GetComponentInChildren<Renderer>().material = greenMaterial;
    }

    void Death(int id)
    {
        if(id != enemyDataInstance.UnitID) { return; }

        GameObject clone = Instantiate(enemyDataInstance.Drop, transform.position, Quaternion.identity);
        clone.GetComponent<Item>().SetData(enemyDataInstance.Data);

        transform.GetComponent<NavMeshAgent>().enabled = false;

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }

    public void DealDamage()
    {
        if (targetUnit)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);

            if (distanceToTarget <= enemyDataInstance.AttackRange)
            {
                DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), enemyDataInstance.BasicAttackDamage);
            }
        }
    }

    public void PlayIdleAnimation()
    {
        // Idle.Play()
    }

    public void StopIdleAnimation()
    {
        // Idle.Stop()
    }

    public void PlayPatrolAnimation()
    {
        // Walk.Play()
    }

    public void StopPatrolAnimation()
    {
        // Walk.Stop()
    }

    public void PlayChaseAnimation()
    {
        // Run.Play()
    }

    public void StopChaseAnimation()
    {
        // Run.Stop()
    }

    public void PlayAttackAnimation()
    {
        // Attack.Play()
    }

    public void StopAttackAnimation()
    {
        // Attack.Stop()
    }


}
