using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;

public class Enemy : MonoBehaviour
{
    [Header("Unit Data")]
    [Tooltip("Assign in inspector ONLY if this object will be one of the Mini-bosses")]
    [SerializeField] protected BossUnitData bossDataInstance;
    protected EnemyUnitData enemyDataInstance;

    protected GameObject targetUnit;

    [Header("Health UI")]
    public Image hpBar;

    protected bool canAttack = true;
    protected bool isAttackDone = false;
    protected Vector3 home;
    protected bool isAlive = true;

    [Header("Animation")]
    protected Animator animator;
    [SerializeField] protected Transform spriteTransform;

    protected virtual void OnEnable()
    {
        if (enemyDataInstance != null)
            DamageHandler.OnEnemyUnitDeath += Death;
        else if (bossDataInstance != null)
            DamageHandler.OnBossUnitDeath += Death;
    }

    protected virtual void OnDisable()
    {
        if (enemyDataInstance != null)
            DamageHandler.OnEnemyUnitDeath -= Death;
        else if (bossDataInstance != null)
            DamageHandler.OnBossUnitDeath -= Death;
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

    public virtual GameObject GetTargetUnit()
    {
        return targetUnit;
    }
    public virtual UnitData GetUnitData()
    {
        if (enemyDataInstance != null) return enemyDataInstance;
        if (bossDataInstance != null) return bossDataInstance;

        return null;
    }

    protected virtual void Death(int id) { }

    protected virtual void Death(Artifacts artifact, string name) { }

    protected virtual async void AttackTimer(float attackSpeed)
    {
        await new WaitForSeconds(attackSpeed);
        SetCanAttack(true);
    }
}
