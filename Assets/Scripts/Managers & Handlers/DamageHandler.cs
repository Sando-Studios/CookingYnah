using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageHandler
{
    public static event Action<int> OnEnemyUnitDeath;
    public static event Action<int> OnPlayerUnitDeath;

    // Damaging a Enemy Unit
    public static void ApplyDamage(Enemy enemy, int damage)
    {
        if (enemy == null) 
            return;

        EnemyUnitData unit = enemy.GetEnemyUnitData();

        unit.CurrentHealth -= damage;
        enemy.Hit();
        enemy.ShowHPBar();

        if (unit.CurrentHealth <= 0)
        {
            unit.CurrentHealth = 0;

            OnEnemyUnitDeath?.Invoke(unit.UnitID);
        }
    }
    // Damaging the Player Unit
    public static void ApplyDamage(Player player, int damage)
    {
        if (player == null)
            return;

        PlayerUnitData unit = player.GetPlayerData();

        // Damage calculations here

        unit.CurrentHealth -= damage;

        UIManager.instance.UpdateHpUI();
        //Play Damage animation/sfx


        if (unit.CurrentHealth <= 0)
        {
            unit.CurrentHealth = 0;

            OnPlayerUnitDeath?.Invoke(0);
        }
    }
}
