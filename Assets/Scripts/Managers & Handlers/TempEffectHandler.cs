using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffectHandler : MonoBehaviour
{
    private TargetStat statName;
    private int amount;
    private float effectDuration = 3.0f;

    public void SetData(TargetStat stat, int value, float duration)
    {
        statName = stat;
        amount = value;
        effectDuration = duration;

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
