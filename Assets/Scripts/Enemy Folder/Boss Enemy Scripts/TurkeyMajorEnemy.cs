using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyMajorEnemy : MajorEnemy
{
    public override void ExecuteSpecialAttack()
    {
        LaunchDelay();
    }

    private async void LaunchDelay()
    {   
        FireMeatballAbility fireBall = GetComponent<FireMeatballAbility>();
        fireBall.SpawnMeatballMeteor(targetUnit.transform.position, bossDataInstance.SpecialAttackDamage);

        await new WaitForSeconds(1.0f);

        fireBall.SpawnMeatballMeteor(targetUnit.transform.position, bossDataInstance.SpecialAttackDamage);

        AttackTimer(bossDataInstance.SpecialAttackSpeed);
        
        SetIsAttackDone(true);
    }

}
