using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingState : MonsterState
{
    private static WanderingState instance;
    public static WanderingState Instance
    {
        get
        {
            if (instance == null)
                instance = new WanderingState();
            return instance;
        }
    }

    private Enemy self;
    private Vector3 targetDestination;

    public override void EnterState(MonsterStateMachine stateMachine)
    {
        //Debug.Log("Wander Enter");
        this.stateMachine = stateMachine;

        self = stateMachine.self;
        self.ResetAggro();

        agent = self.gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2.0f;
        agent.speed = self.GetEnemyUnitData().WanderSpeed;

        targetDestination = GetRandomVector(self.GetHome());
        SetNewDestination();
    }

    public override void UpdateState()
    {
        //Debug.Log(agent.isStopped);

        float distance = Vector3.Distance(self.transform.position, agent.destination);

        if (distance < 3.0f)
            stateMachine.TransitionToState(IdleState.Instance);
        if (self.GetTargetUnit() != null)
            stateMachine.TransitionToState(ChasingState.Instance);
    }

    private void SetNewDestination()
    {
        //Debug.Log(targetDestination);
        agent.SetDestination(targetDestination);
    }

    private Vector3 GetRandomVector(Vector3 original)
    {
        float range = 5f;

        float offsetX = Random.Range(-range, range);
        float offsetZ = Random.Range(-range, range);

        Vector3 randomVector = new Vector3(original.x + offsetX, original.y, original.z + offsetZ);

        return randomVector;
    }

    public override void ExitState()
    {

    }
}
