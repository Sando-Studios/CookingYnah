using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageHandler
{
    public static event Action<int> OnEnemyUnitDeath;
    public static event Action<Artifacts, string> OnBossUnitDeath;
    public static event Action<int> OnPlayerUnitDeath;

    // Damaging a Minor Enemy or Major Enemy Unit
    public static void ApplyDamage(Enemy enemy, int baseDamage, float playerStrength)
    {
        if (enemy == null)
            return;

        UnitData unit = enemy.GetUnitData();

        // Damage calculations
        float actualDamage = baseDamage + playerStrength;
        int finalDamage = Mathf.RoundToInt(actualDamage);

        unit.CurrentHealth -= finalDamage;
        enemy.Hit();

        if (unit.CurrentHealth <= 0)
        {
            unit.CurrentHealth = 0;

            if (unit is EnemyUnitData)
            {
                EnemyUnitData enemyData = unit as EnemyUnitData;
                OnEnemyUnitDeath ?.Invoke(enemyData.UnitID);
            }
            else if (unit is BossUnitData)
            {
                BossUnitData bossData = unit as BossUnitData;
                OnBossUnitDeath?.Invoke(bossData.Artifact, bossData.UnitName);
            }

        }
    }

    // Damaging the Player Unit
    public static void ApplyDamage(Player player, int baseDamage)
    {
        if (player == null)
            return;

        PlayerUnitData unit = player.GetPlayerData();

        // Damage calculations

        float damageReduction = 1 - (1 / (1 + unit.Resilience * unit.ResDiminishingFactor));

        float actualDamage = baseDamage * damageReduction;
        int finalDamage = Mathf.RoundToInt(actualDamage);

        unit.CurrentHealth -= finalDamage;

        //UIManager.instance.UpdateHpUI();
        //Play Damage animation/sfx


        if (unit.CurrentHealth <= 0)
        {
            unit.CurrentHealth = 0;
            OnPlayerUnitDeath?.Invoke(0);
        }
    }
}
