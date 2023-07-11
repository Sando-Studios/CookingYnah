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
        ResetVariables();

        enemy.ControlAnimations(MonsterStates.Attack, true);
    }

    public override void Update(float deltaTime)
    {
        if (!enemy.GetTargetUnit())
        {
            statManager.ChangeState(enemy, new IdleState(statManager, enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.GetTargetUnit().transform.position);

        if (!enemy.IsAlive())
        {
            statManager.ChangeState(enemy, new DeathState(statManager, enemy));
            return;
        }
        if (distanceToTarget > enemy.GetEnemyUnitData().AttackRange)
            statManager.ChangeState(enemy, new ChaseState(statManager, enemy));
        else
        {
            attackTimer += deltaTime;

            if (!hasAttacked && attackTimer >= enemy.GetEnemyUnitData().AttackSpeed)
            {
                hasAttacked = true;
                enemy.DoAttack();

                ResetVariables();
            }
        }
    }

    void ResetVariables()
    {
        hasAttacked = false;
        attackTimer = 0f;
    }
    public override void Exit()
    {
        enemy.ControlAnimations(MonsterStates.Attack, false);
    }
}
