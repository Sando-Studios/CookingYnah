using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private PlayerUnitData player;
    public GameObject tempEffectHandlerPrefab;

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

    public void ApplyTempBuffs(TargetStat stat, int value, float duration)
    {
        GameObject clone = Instantiate(tempEffectHandlerPrefab, transform);
        clone.GetComponent<TempEffectHandler>().SetData(stat, value, duration);

    }

    public void ApplyBuff(TargetStat stat, int value)
    {
        if (stat == TargetStat.VitStat) player.Vitality += value;
        else if (stat == TargetStat.AgiStat) player.Agility += value;
        else if (stat == TargetStat.StrStat) player.Strength += value;
        else if (stat == TargetStat.VigStat) player.Vigor += value;
        else if (stat == TargetStat.IntStat) player.Intelligence += value;
        else if (stat == TargetStat.EndStat) player.Endurance += value;
        else if (stat == TargetStat.DexStat) player.Dexterity += value;
        else Debug.Log("Invalid stat: " + stat);

        UIManager.instance.UpdateStatsUI();
    }

    public void RemoveBuff(TargetStat stat, int value)
    {
        if (stat == TargetStat.VitStat) player.Vitality -= value;
        else if (stat == TargetStat.AgiStat) player.Agility -= value;
        else if (stat == TargetStat.StrStat) player.Strength -= value;
        else if (stat == TargetStat.VigStat) player.Vigor -= value;
        else if (stat == TargetStat.IntStat) player.Intelligence -= value;
        else if (stat == TargetStat.EndStat) player.Endurance -= value;
        else if (stat == TargetStat.DexStat) player.Dexterity -= value;
        else Debug.Log("Invalid stat: " + stat);

        UIManager.instance.UpdateStatsUI();
    }


}
