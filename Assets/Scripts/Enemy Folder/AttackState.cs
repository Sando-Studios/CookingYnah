using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonsterState
{
    private float attackTimer;
    private bool hasAttacked;

    public AttackState(MonsterStateManager manager, Enemy enemy) : base(manager, enemy) { }

    public override void Enter()
    {
        attackTimer = 0f;
        hasAttacked = false;

        enemy.PlayAttackAnimation();
    }

    public override void Update(float deltaTime)
    {
        if (!enemy.GetTargetUnit())
        {
            statManager.ChangeState(enemy, new IdleState(statManager, enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.GetTargetUnit().transform.position);

        if (distanceToTarget > enemy.GetEnemyUnitData().AttackRange)
            statManager.ChangeState(enemy, new ChaseState(statManager, enemy));
        else
        {
            attackTimer += deltaTime;

            if (!hasAttacked && attackTimer >= enemy.GetEnemyUnitData().AttackSpeed)
            {
                enemy.DealDamage();
                hasAttacked = true;
            }
        }
    }

    public override void Exit()
    {
        enemy.StopAttackAnimation();
    }
}
