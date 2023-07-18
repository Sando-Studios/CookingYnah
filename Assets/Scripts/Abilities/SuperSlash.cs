using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SuperSlash : MonoBehaviour
{
    private List<Enemy> enemiesInRangeList;
    private Player playerInRange;

    private bool isBoss;

    private int damageValue;
    private float strength;

    public void SetDamageValue(float dmg)
    {
        damageValue = (int)dmg;
    }
    public void SetDamageValue(float dmg, float strength)
    {
        SetDamageValue((int)dmg);
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
            else if (!isBoss && other.CompareTag("Enemy"))
            {
                enemiesInRangeList.Add(other.GetComponent<Enemy>());
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
                enemiesInRangeList.Remove(other.GetComponent<Enemy>());
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
            foreach (Enemy e in enemiesInRangeList)
            {
                DealDamage(e);
            }
        }
    }

    private void DealDamage(Player player)
    {
        DamageHandler.ApplyDamage(player, damageValue);
    }
    private void DealDamage(Enemy enemy)
    {
        DamageHandler.ApplyDamage(enemy, damageValue, strength);
    }
}
