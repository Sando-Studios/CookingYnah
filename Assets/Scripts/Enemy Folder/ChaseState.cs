using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonsterState
{
    private NavMeshAgent navMeshAgent;

    public ChaseState(MonsterStateManager manager, MinorEnemy enemy) : base(manager, enemy)
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        navMeshAgent.speed = enemy.GetUnitData().ChaseSpeed;

        enemy.ControlAnimations(MonsterStates.Chase, true);
    }

    public override void Update(float deltaTime)
    {
        if (!enemy.IsAlive())
        {
            statManager.ChangeState(enemy, new DeathState(statManager, enemy));
            return;
        }

        if (enemy.GetTargetUnit() == null)
        {
            statManager.ChangeState(enemy, new PatrolState(statManager, enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.GetTargetUnit().transform.position);

        if (distanceToTarget <= enemy.GetUnitData().AttackRange)
        {
            statManager.ChangeState(enemy, new CombatState(statManager, enemy));
            return;
        }
        else if (distanceToTarget > enemy.GetUnitData().ChaseRange)
        {
            statManager.ChangeState(enemy, new PatrolState(statManager, enemy));
            return;
        }
        else
        {
            navMeshAgent.SetDestination(enemy.GetTargetUnit().transform.position);
            return;
        }
    }

    public override void Exit()
    {
        enemy.ResetAggro();
        enemy.ControlAnimations(MonsterStates.Chase, false);
    }
}
