using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SuperSlash : MonoBehaviour
{
    private List<MinorEnemy> enemiesInRangeList;
    private Player playerInRange;

    private bool isBoss;

    private int damageValue;
    private float strength;

    public void SetDamageValue(float dmg)
    {
        damageValue = (int)dmg;
        isBoss = true;
    }
    public void SetDamageValue(float dmg, float strength)
    {
        SetDamageValue((int)dmg);
        isBoss = false;
        this.strength = strength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (isBoss && other.CompareTag("Player"))
            {
                playerInRange = other.GetComponent<Player>();
            }
            else if (!isBoss && other.CompareTag("MinorEnemy"))
            {
                enemiesInRangeList.Add(other.GetComponent<MinorEnemy>());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            if (isBoss && other.CompareTag("Player"))
            {
                playerInRange = null;
            }
            else if (!isBoss && other.CompareTag("Enemy"))
            {
                enemiesInRangeList.Remove(other.GetComponent<MinorEnemy>());
            }
        }
    }

    public void TriggerDamage()
    {
        if (isBoss && playerInRange)
        {
            DealDamage(playerInRange);
        }
        else if (!isBoss && enemiesInRangeList.Count > 0)
        {
            foreach (MinorEnemy e in enemiesInRangeList)
            {
                DealDamage(e);
            }
        }
    }

    private void DealDamage(Player player)
    {
        DamageHandler.ApplyDamage(player, damageValue);
    }
    private void DealDamage(MinorEnemy enemy)
    {
        DamageHandler.ApplyDamage(enemy, damageValue, strength);
    }
}
