using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterState
{
    protected MonsterStateMachine stateMachine;
    protected NavMeshAgent agent;
    public abstract void EnterState(MonsterStateMachine stateMachine);
    public abstract void UpdateState();
    public abstract void ExitState();
}
