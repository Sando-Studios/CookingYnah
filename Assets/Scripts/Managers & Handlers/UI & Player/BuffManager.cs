using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private PlayerUnitData player;
    public GameObject tempEffectHandlerPrefab;

    private Dictionary<TargetStat, int> tempBuffAmountDictionary = new Dictionary<TargetStat, int>
    {
        { TargetStat.VitStat, 0 },
        { TargetStat.StrStat, 0 },
        { TargetStat.ResStat, 0 },
    };

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

        tempBuffAmountDictionary[stat] += value;
    }

    public void ApplyBuff(TargetStat stat, int value)
    {
        var s = stat;
        switch(s){
            case TargetStat.VitStat:
                player.Vitality += value;
                break;
            case TargetStat.StrStat:
                player.Strength += value;
                break;
            case TargetStat.ResStat:
                player.Resilience += value;
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
                GuardMaxHealth();
                UIManager.instance.UpdateHpBarUI();
                break;
            case TargetStat.StrStat:
                player.Strength -= value;
                break;
            case TargetStat.ResStat:
                player.Resilience -= value;
                break;
            default:
                Debug.Log("Invalid stat: " + stat);
                break;
        }

        tempBuffAmountDictionary[stat] -= value;
        UIManager.instance.UpdateStatsUI();
    }

    public void RemoveAllTempBuffs()
    {
        player.Vitality -= tempBuffAmountDictionary[TargetStat.VitStat];

        player.Strength -= tempBuffAmountDictionary[TargetStat.StrStat];

        player.Resilience -= tempBuffAmountDictionary[TargetStat.ResStat];

        if (player.Vitality < 1)
            player.Vitality = 1;
        
        if (player.Strength < 1)
            player.Strength = 1;
        
        if (player.Resilience < 1)
            player.Resilience = 1;

        /*foreach (var key in tempBuffAmountDictionary.Keys)
        {
            tempBuffAmountDictionary[key] = 0;
        }*/
        tempBuffAmountDictionary[TargetStat.ResStat] = 0;
        tempBuffAmountDictionary[TargetStat.VitStat] = 0;
        tempBuffAmountDictionary[TargetStat.StrStat] = 0;
        
        GuardMaxHealth();
    } 

    public void ApplyHeal(int value)
    {
        player.CurrentHealth += value;

        GuardMaxHealth();

        UIManager.instance.UpdateHpBarUI();
    }

    private void GuardMaxHealth()
    {
        if (player.CurrentHealth > player.MaxHealth)
        {
            player.CurrentHealth = player.MaxHealth;
        }
    }
}
