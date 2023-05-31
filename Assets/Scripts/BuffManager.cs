using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private PlayerUnitData player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer(PlayerUnitData unit)
    {
        player = unit;
    }

    public void ApplyBuff(TargetStat stat, int value)
    {
        if      (stat == TargetStat.VitStat) { ApplyVitalityBuff(value); }
        else if (stat == TargetStat.AgiStat) { ApplyAgilityBuff(value); }
        else if (stat == TargetStat.StrStat) { ApplyStrengthBuff(value); }
        else if (stat == TargetStat.VigStat) { ApplyVigorBuff(value); }
        else if (stat == TargetStat.IntStat) { ApplyIntelligenceBuff(value); }
        else if (stat == TargetStat.EndStat) { ApplyEnduranceBuff(value); }
        else if (stat == TargetStat.DexStat) { ApplyDexterityBuff(value); }

        UIManager.instance.UpdateStatsUI();
    }

    void ApplyVitalityBuff(int value)
    {
        player.Vitality += value;
    }
    void ApplyAgilityBuff(int value)
    {
        player.Agility += value;
    }

    void ApplyStrengthBuff(int value)
    {
        player.Strength += value;
    }

    void ApplyVigorBuff(int value)
    {
        player.Vigor += value;
    }

    void ApplyIntelligenceBuff(int value)
    {
        player.Intelligence += value;
    }

    void ApplyEnduranceBuff(int value)
    {
        player.Endurance += value;
    }

    void ApplyDexterityBuff(int value)
    {
        player.Dexterity += value;
    }
}
