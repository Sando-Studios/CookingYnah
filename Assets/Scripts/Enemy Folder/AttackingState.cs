using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : MonsterState
{
    private static AttackingState instance;
    public static AttackingState Instance
    {
        get
        {
            if (instance == null)
                instance = new AttackingState();
            return instance;
        }
    }

    private Enemy self;
    private GameObject target;
    private bool isAttacking = false;

    public override void EnterState(MonsterStateMachine stateMachine)
    {
        //Debug.Log("Attack Enter");

        this.stateMachine = stateMachine;

        self = stateMachine.self;

        target = self.GetTargetUnit();
    }

    public override void UpdateState()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Attack();
        }

        float distanceToTarget = Vector3.Distance(self.transform.position, target.transform.position);

        if (distanceToTarget > self.GetEnemyUnitData().AttackRange)
            stateMachine.TransitionToState(ChasingState.Instance);

    }

    private async void Attack()
    {
        if (target)
        {
            float distanceToTarget = Vector3.Distance(self.transform.position, target.transform.position);

            if (distanceToTarget <= self.GetEnemyUnitData().AttackRange)
            {
                DamageHandler.ApplyDamage(target.GetComponent<Player>(), self.GetEnemyUnitData().BasicAttackDamage);
            }
        }

        await new WaitForSeconds(self.GetEnemyUnitData().AttackSpeed);

        isAttacking = false;
    }

    public override void ExitState()
    {


    }
}
