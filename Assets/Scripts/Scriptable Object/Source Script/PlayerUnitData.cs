using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerUnit", menuName = "Unit/Player")]
public class PlayerUnitData : UnitData
{
    [SerializeField] private int baseHealth = 10;
    [Header("Vitality to HP Scaling Factor")]
    [SerializeField] private float vitHpScalingFactor = 0.5f;

    private int vitality = 1;
    private int strength = 1;
    private int resilience = 1;

    [Header("Max Damage Reduction")] [Range(0f, 1f)] [Tooltip("The closer to 1, the  higher max amount of damage reduction")]
    [SerializeField] float maxDamageReduction = 0.9f;

    [SerializeField] private float maxStamina = 100.0f;
    private float currentStamina;

    [SerializeField] private float attackRange = 5.0f;

    [SerializeField] private int rawDamage = 5;
    

    public int MaxHealth
    {
        get { return baseHealth + Mathf.RoundToInt(Vitality * vitHpScalingFactor); }
    }

    public int Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public int Resilience
    {
        get { return resilience; }
        set { resilience = value; }
    }
    public float MaxDamageReduction
    {
        get { return 1 - maxDamageReduction; }
        set { maxDamageReduction = value; }
    }

    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }
    public float CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public int RawDamage
    {
        get { return rawDamage; }
        set { rawDamage = value; }
    }
}
