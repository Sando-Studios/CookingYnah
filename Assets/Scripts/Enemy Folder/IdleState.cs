using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using UnityEngine.AI;
using System.Threading;

public class IdleState : MonsterState
{
    private float animationDuration;
    private float animationTimer;
    public IdleState(MonsterStateManager manager, Enemy enemy) : base(manager, enemy) { }

    public override void Enter()
    {
        animationDuration = Random.Range(1f, 4f);
        animationTimer = 0f;

        enemy.PlayIdleAnimation();
    }

    public override void Update(float deltaTime)
    {
        animationTimer += deltaTime;

        if (enemy.GetTargetUnit())
        {
            statManager.ChangeState(enemy, new ChaseState(statManager, enemy));
            return;
        }

        if (animationTimer >= animationDuration)
        {
            statManager.ChangeState(enemy, new PatrolState(statManager, enemy));
        }
    }

    public override void Exit()
    {
        enemy.StopIdleAnimation();
    }
}
