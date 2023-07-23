using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyMajorEnemy : MajorEnemy
{
    public override void ExecuteSpecialAttack()
    {
        Vector3 direction = targetUnit.transform.position - transform.position;
        direction.Normalize();
        spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));
    }

    public void SpawnAttack()
    {
        FireMeatballAbility fireBall = GetComponent<FireMeatballAbility>();
        fireBall.SpawnMeatballMeteor(targetUnit.transform.position, bossDataInstance.SpecialAttackDamage);

        PlayAudioClip(GetAudioClipName("SpecialA"));

        AttackTimer(bossDataInstance.SpecialAttackSpeed);
    }

}
