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
        patrolPoint = enemy.GetRandomPatrolPoint();
        agent.SetDestination(patrolPoint);

        agent.speed = enemy.GetEnemyUnitData().PatrolSpeed;

        enemy.ControlAnimations(MonsterStates.Patrol, true);
    }

    public override void Update(float deltaTime)
    {
        if (!enemy.IsAlive())
        {
            statManager.ChangeState(enemy, new DeathState(statManager, enemy));
            return;
        }

        if (enemy.GetTargetUnit() != null)
        {
            statManager.ChangeState(enemy, new ChaseState(statManager, enemy));
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            statManager.ChangeState(enemy, new IdleState(statManager, enemy));
            return;
        }
    }

    public override void Exit()
    {
        enemy.ControlAnimations(MonsterStates.Patrol, false);
    }
}

