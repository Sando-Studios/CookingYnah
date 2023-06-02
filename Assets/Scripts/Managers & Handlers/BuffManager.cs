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
        var s = stat;
        switch(s){
            case TargetStat.VitStat:
                player.Vitality += value;
                break;
            case TargetStat.AgiStat:
                player.Agility += value;
                break;
            case TargetStat.StrStat:
                player.Strength += value;
                break;
            case TargetStat.VigStat:
                player.Vigor += value;
                break;
            case TargetStat.IntStat:
                player.Intelligence += value;
                break;
            case TargetStat.EndStat:
                player.Endurance += value;
                break;
            case TargetStat.DexStat:
                player.Dexterity += value;
                break;
            default:
                Debug.Log("Invalid stat: " + stat);
                break;
        }

        UIManager.instance.UpdateStatsUI();
    }

    public void RemoveBuff(TargetStat stat, int value)
    {
        var s = stat;
        switch (s)
        {
            case TargetStat.VitStat:
                player.Vitality -= value;
                break;
            case TargetStat.AgiStat:
                player.Agility -= value;
                break;
            case TargetStat.StrStat:
                player.Strength -= value;
                break;
            case TargetStat.VigStat:
                player.Vigor -= value;
                break;
            case TargetStat.IntStat:
                player.Intelligence -= value;
                break;
            case TargetStat.EndStat:
                player.Endurance -= value;
                break;
            case TargetStat.DexStat:
                player.Dexterity -= value;
                break;
            default:
                Debug.Log("Invalid stat: " + stat);
                break;
        }

        UIManager.instance.UpdateStatsUI();
    }


}
