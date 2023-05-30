using System.Collections;
using System.Collections.Generic;
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

    public void ApplyVitalityBuff(int value)
    {
        player.Vitality += value;
    }
    public void ApplyAgilityBuff(int value)
    {
        player.Agility += value;
    }

    public void ApplyStrengthBuff(int value)
    {
        player.Strength += value;
    }

    public void ApplyVigorBuff(int value)
    {
        player.Vigor += value;
    }

    public void ApplyIntelligenceBuff(int value)
    {
        player.Intelligence += value;
    }

    public void ApplyEnduranceBuff(int value)
    {
        player.Endurance += value;
    }

    public void ApplyDexterityBuff(int value)
    {
        player.Dexterity += value;
    }
}
