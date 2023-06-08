using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathState : MonsterState
{
    public DeathState(MonsterStateManager manager, Enemy enemy) : base(manager, enemy) { }

    public override void Enter()
    {
        // Play death animation
        //enemy.PlayDeathAnimation();

        // Remove the enemy object from the game
        //Destroy(enemy.gameObject, enemy.DeathAnimationDuration);
    }

    public override void Update(float deltaTime)
    {
        // No need for further updates in the death state
    }

    public override void Exit()
    {
        // No need for exit logic in the death state
    }
}

