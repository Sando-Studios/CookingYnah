using Asyncoroutine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MajorEnemy : Enemy
{
    [Header("Unit Data")]
    protected BossUnitData bossDataInstance;

    [SerializeField] protected GameObject targetUnit;
    protected bool canAttack = true;
    protected bool isAttackDone = false;
    [SerializeField] protected Vector3 home;

    protected NavMeshAgent agent;
    protected bool isAlive = true;

    [Header("Animation")]
    protected Animator animator;
    [SerializeField] protected Transform spriteTransform;

    protected virtual void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += Death;
    }
    protected virtual void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= Death;
    }

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual bool IsAlive()
    {
        return isAlive;
    }
    public virtual bool GetCanAttack()
    {
        return canAttack;
    }
    public virtual void SetCanAttack(bool isAttackPossible)
    {
        canAttack = isAttackPossible;
    }
    public virtual bool GetIsAttackDone()
    {
        return isAttackDone;
    }
    public virtual void SetIsAttackDone(bool hasAttackFinished)
    {
        isAttackDone = hasAttackFinished;
    }

    public virtual async void AttackTimer()
    {
        await new WaitForSeconds(bossDataInstance.AttackSpeed);
        SetCanAttack(true);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            targetUnit = other.gameObject;
            //aggroTrigger.enabled = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float a = bossDataInstance.CurrentHealth;
        float b = bossDataInstance.MaxHealth;
        float normalized = a / b;

        //hpBar.fillAmount = normalized;

        if (agent.hasPath)
        {
            Vector3 direction = agent.velocity.normalized;

            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));
        }
    }


    public virtual void ResetAggro()
    {
        targetUnit = null;
        //aggroTrigger.enabled = true;
    }

    public virtual Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = home + UnityEngine.Random.insideUnitSphere * 7.0f;
        NavMesh.SamplePosition(randomPoint, out NavMeshHit point, 7.0f, NavMesh.AllAreas);
        return point.position;
    }
    public virtual GameObject GetTargetUnit()
    {
        return targetUnit;
    }
    public virtual BossUnitData GetEnemyUnitData()
    {
        return bossDataInstance;
    }

    public virtual async void Hit()
    {
        var r = GetComponentsInChildren<SpriteRenderer>();

        foreach (var m in r)
        {
            m.color = Color.red;
        }
        await new WaitForSeconds(0.5f);

        foreach (var m in r)
        {
            m.color = new Color(255, 255, 255, 255);
        }
    }

    protected virtual void Death(int id)
    {
        if (id != bossDataInstance.UnitID) { return; }

        isAlive = false;

        ItemData data = GetRandomItemData();

        GameObject clone = Instantiate(bossDataInstance.DropObject, transform.position, Quaternion.identity);
        clone.GetComponent<Item>().SetData(data);

        transform.GetComponent<NavMeshAgent>().enabled = false;

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }

    protected virtual ItemData GetRandomItemData()
    {
        float totalWeight = 0f;
        foreach (float dropChance in bossDataInstance.DropData.Values)
        {
            totalWeight += dropChance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        ItemData data = null;
        foreach (var entry in bossDataInstance.DropData)
        {
            randomValue -= entry.Value;
            if (randomValue <= 0f)
            {
                data = entry.Key;
                break;
            }
        }

        return data;
    }

    public virtual void ExecuteAttack()
    {
        if (targetUnit)
        {
            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.Normalize();
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            AttackTimer();
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), bossDataInstance.BasicAttackDamage);
        }
    }

    public virtual void ControlAnimations(MonsterStates state, bool isPlaying)
    {
        ResetAnimatorBool();

        var s = state;
        switch (s)
        {
            case MonsterStates.Attack:
                animator.SetBool("isAttacking", isPlaying);
                break;
            case MonsterStates.Combat:
                animator.SetBool("isInCombat", isPlaying);
                break;
            case MonsterStates.Chase:
                animator.SetBool("isChasing", isPlaying);
                break;
            case MonsterStates.Idle:
                animator.SetBool("isIdling", isPlaying);
                break;
            case MonsterStates.Patrol:
                animator.SetBool("isPatrolling", isPlaying);
                break;

        }
    }

    protected virtual void ResetAnimatorBool()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isInCombat", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isIdling", false);
        animator.SetBool("isPatrolling", false);
    }
}
