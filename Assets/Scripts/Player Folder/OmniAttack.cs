using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniAttack : MonoBehaviour
{
    private List<MinorEnemy> enemiesInRange = new List<MinorEnemy>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MinorEnemy" && !other.isTrigger)
        {
            MinorEnemy enemy = other.GetComponent<MinorEnemy>();
            enemiesInRange.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MinorEnemy" && !other.isTrigger)
        {
            enemiesInRange.Remove(other.GetComponent<MinorEnemy>());
        }
    }

    public void DealDamage(int damage)
    {
        foreach (MinorEnemy enemy in enemiesInRange)
        {
            DamageHandler.ApplyDamage(enemy, damage, GetComponent<Player>().GetPlayerData().Strength);
        }
    } 
}
