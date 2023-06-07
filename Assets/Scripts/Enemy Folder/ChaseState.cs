using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonsterState
{
    private NavMeshAgent navMeshAgent;

    public ChaseState(MonsterStateManager manager, Enemy enemy) : base(manager, enemy)
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        navMeshAgent.speed = enemy.GetEnemyUnitData().ChaseSpeed;

        enemy.ControlAnimations(MonsterStates.Chase, true);
    }

    public override void Update(float deltaTime)
    {
        if (!enemy.GetTargetUnit())
        {
            statManager.ChangeState(enemy, new PatrolState(statManager, enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.GetTargetUnit().transform.position);

        if (distanceToTarget <= enemy.GetEnemyUnitData().AttackRange)
            statManager.ChangeState(enemy, new AttackState(statManager, enemy));
        else if (distanceToTarget > enemy.GetEnemyUnitData().ChaseRange)
            statManager.ChangeState(enemy, new PatrolState(statManager, enemy));
        else
            navMeshAgent.SetDestination(enemy.GetTargetUnit().transform.position);
    }

    public override void Exit()
    {
        enemy.ControlAnimations(MonsterStates.Chase, false);
    }
}
