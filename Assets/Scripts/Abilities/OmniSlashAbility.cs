using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniSlashAbility : ArtifactAbility
{
    private GameObject cloneRef;
    
    private void Start()
    {
        cloneRef = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        cloneRef.transform.SetParent(gameObject.transform);
    }

    private void SpawnPlayerSlashZone()
    {
        isAbilityActive = true;
        SuperSlash superSlash = cloneRef.GetComponent<SuperSlash>();
        
        superSlash.SetDamageValue(GetComponent<Player>().GetPlayerData().RawDamage, GetComponent<Player>().GetPlayerData().Strength);
        superSlash.TriggerDamage();
        isAbilityActive = false;
    }
    public void SpawnBossSlashZone(float damage)
    {
        if (GetComponent<MajorEnemy>().GetBossState() != BossState.SpecialAttack) return;

        GameObject clone = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        clone.transform.SetParent(gameObject.transform);
        SuperSlash superSlash = clone.GetComponent<SuperSlash>();

        superSlash.SetDamageValue(damage);
        superSlash.TriggerDamage();
    }

    protected override void UseSpecialAttack()
    {
        SpawnPlayerSlashZone();
    }
}
