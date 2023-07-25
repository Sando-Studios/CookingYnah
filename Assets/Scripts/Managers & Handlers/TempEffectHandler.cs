using System;
using Asyncoroutine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffectHandler : MonoBehaviour
{
    private TargetStat statName;
    private int amount;
    private float effectDuration = 3.0f;

    private void OnEnable()
    {
        DamageHandler.OnPlayerUnitDeath += OnPlayerKilled;
    }

    private void OnDisable()
    {
        DamageHandler.OnPlayerUnitDeath -= OnPlayerKilled;
    }

    private void OnPlayerKilled(int i)
    {
        amount = 0;
    }
    public static Action<TargetStat, float> OnNewTempBuff;

    public void SetData(TargetStat stat, int value, float duration)
    {
        statName = stat;
        amount = value;
        effectDuration = duration;

        OnNewTempBuff?.Invoke(stat, duration);
        EffectTimer();
    }

    private async void EffectTimer()
    {
        BuffManager.instance.ApplyBuff(statName, amount);
        await new WaitForSeconds(effectDuration);
        BuffManager.instance.RemoveBuff(statName, amount);

        Destroy(gameObject);
    }
}
