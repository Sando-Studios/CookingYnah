using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonsterState
{
    private Vector3 patrolPoint;

    public PatrolState(MonsterStateManager manager, Enemy enemy) : base(manager, enemy)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        patrolPoint = GetRandomPatrolPoint();
        agent.SetDestination(patrolPoint);

        agent.speed = enemy.GetEnemyUnitData().PatrolSpeed;

        enemy.ControlAnimations(MonsterStates.Patrol, true);
    }

    public override void Update(float deltaTime)
    {
        if (enemy.GetTargetUnit())
        {
            statManager.ChangeState(enemy, new ChaseState(statManager, enemy));
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            statManager.ChangeState(enemy, new IdleState(statManager, enemy));
        }
    }

    public override void Exit()
    {
        enemy.ControlAnimations(MonsterStates.Patrol, false);
    }

    private Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = enemy.GetHome() + Random.insideUnitSphere * 4.0f;
        NavMesh.SamplePosition(randomPoint, out NavMeshHit point, 4.0f, NavMesh.AllAreas);
        return point.position;
    }
}

