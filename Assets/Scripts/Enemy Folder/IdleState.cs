using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : MonsterState
{
    private float animationDuration;
    private float animationTimer;
    public IdleState(MonsterStateManager manager, MinorEnemy enemy) : base(manager, enemy) { }

    public override void Enter()
    {
        animationDuration = Random.Range(1f, 4f);
        animationTimer = 0f;

        enemy.ControlAnimations(MonsterStates.Idle, true);
    }

    public override void Update(float deltaTime)
    {
        animationTimer += deltaTime;

        enemy.PlaySoundRandomTime("Idle");

        if (!enemy.IsAlive())
        {
            statManager.ChangeState(enemy, new DeathState(statManager, enemy));
            return;
        }

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
        enemy.ControlAnimations(MonsterStates.Idle, false);
    }
}
