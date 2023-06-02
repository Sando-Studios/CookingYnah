using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageHandler
{
    // Damaging a Unit
    public static void TakeDamage(UnitData unit,  int damage)
    {
        if(unit != null)
        {
            unit.CurrentHealth -= damage;
            //unit.StartCoroutine(Hit());

            if (unit.CurrentHealth <= 0)
            {
                unit.CurrentHealth = 0;
                //unit.EventHandler.Death(attacker);
            }
            return;
        }
    }
}
