using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class OmniSlashAbility : ArtifactAbility
{

    private void SpawnPlayerSlashZone()
    {
        GameObject clone = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        clone.transform.SetParent(gameObject.transform);
        SuperSlash superSlash = clone.GetComponent<SuperSlash>();
        
        superSlash.SetDamageValue(GetComponent<Player>().GetPlayerData().RawDamage, GetComponent<Player>().GetPlayerData().Strength);
    }
    public void SpawnBossSlashZone(float damage)
    {
        GameObject clone = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        clone.transform.SetParent(gameObject.transform);
        SuperSlash superSlash = clone.GetComponent<SuperSlash>();

        superSlash.SetDamageValue(damage); 
    }

    protected override void UseSpecialAttack()
    {
        SpawnPlayerSlashZone();
    }
}
