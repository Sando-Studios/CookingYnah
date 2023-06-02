using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : MonsterState
{
    private static ChasingState instance;
    public static ChasingState Instance
    {
        get
        {
            if (instance == null)
                instance = new ChasingState();
            return instance;
        }
    }

    private Enemy self;
    private GameObject target;

    public override void EnterState(MonsterStateMachine stateMachine)
    {
        //Debug.Log("Chase Enter");
        this.stateMachine = stateMachine;
        
        self = stateMachine.self;

        target = self.GetTargetUnit();

        agent = self.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = self.GetEnemyUnitData().ChaseSpeed;
    }

    public override void UpdateState()
    {
        agent.destination = target.transform.position;


        float distanceToTarget = Vector3.Distance(self.transform.position, target.transform.position);

        if (distanceToTarget <= self.GetEnemyUnitData().AttackRange)
            stateMachine.TransitionToState(AttackingState.Instance);
        else if (distanceToTarget > self.GetEnemyUnitData().ChaseRange)
            stateMachine.TransitionToState(WanderingState.Instance);
    }

    public override void ExitState()
    {
        agent.speed = self.GetEnemyUnitData().WanderSpeed;
        
    }
}
