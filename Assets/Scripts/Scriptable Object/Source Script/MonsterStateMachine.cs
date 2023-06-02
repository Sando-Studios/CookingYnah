using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Monster Mob State Machine")]
public class MonsterStateMachine : ScriptableObject
{
    public MonsterState currentState;
    public Enemy self;

    public void Initialize(Enemy enemy)
    {
        self = enemy;
        TransitionToState(WanderingState.Instance);
    }

    public void UpdateState()
    {
        currentState.UpdateState();
    }

    public void TransitionToState(MonsterState nextState)
    {
        if (nextState == null)
            return;

        if (currentState != null)
            currentState.ExitState();

        currentState = nextState;
        currentState.EnterState(this);
    }
}
