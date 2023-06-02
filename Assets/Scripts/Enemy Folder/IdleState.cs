using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Asyncoroutine;
using UnityEngine.AI;

public class IdleState : MonsterState
{
    private static IdleState instance;
    public static IdleState Instance
    {
        get
        {
            if (instance == null)
                instance = new IdleState();
            return instance;
        }
    }
    private Enemy self;
    private GameObject target;
    private float idleTimer;
    private bool isDone = false;

    public override void EnterState(MonsterStateMachine stateMachine)
    {
        Debug.Log("Idle Enter");
        this.stateMachine = stateMachine;

        self = stateMachine.self;

        agent = self.gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        idleTimer = Random.Range(1.0f, 4.0f);
        IdleWait();
    }

    public override void UpdateState()
    {
        // Do Idle animations a few times

        if (self.GetTargetUnit() != null)
            stateMachine.TransitionToState(ChasingState.Instance);
        else if (isDone)
            stateMachine.TransitionToState(WanderingState.Instance);
    }

    private async void IdleWait()
    {
        await new WaitForSeconds(idleTimer);
        isDone = true;
        Debug.Log("Idle Done");
    }

    public override void ExitState()
    {
        isDone = false;
        agent.isStopped = false;
    }
}
