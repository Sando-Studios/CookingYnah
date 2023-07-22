using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonsterState
{
    public AttackState(MonsterStateManager manager, MinorEnemy enemy) : base(manager, enemy) { }

    public override void Enter()
    {
        enemy.ControlAnimations(MonsterStates.Attack, true);
    }

    public override void Update(float deltaTime)
    {
        if (enemy.GetCanAttack())
        {
            enemy.PlaySoundRandomTime("Attack");
            enemy.SetCanAttack(false);
            enemy.SetIsAttackDone(false);
            enemy.ExecuteAttack();
        }

        if (!enemy.GetCanAttack() && enemy.GetIsAttackDone())
        {
            statManager.ChangeState(enemy, new CombatState(statManager, enemy));
            return;
        }

    }
    public override void Exit()
    {
        enemy.ControlAnimations(MonsterStates.Attack, false);
    }
}
