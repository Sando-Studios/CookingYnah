using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerUnit", menuName = "Unit/Player")]
public class PlayerUnitData : UnitData
{
    [SerializeField] private float vitality;
    [SerializeField] private float agility;
    [SerializeField] private float strength;
    [SerializeField] private float vigor;
    [SerializeField] private float intelligence;
    [SerializeField] private float endurance;
    [SerializeField] private float dexterity;

    [SerializeField] private float attackRange;

    private float currentSprintStamina;
    private float currentBlockMeter;

    public override int MaxHealth
    {
        get { return (int)(Vitality * 10); }
    }
    public override int CurrentHealth
    {
        get { return base.CurrentHealth; }
        set { base.CurrentHealth = value; }
    }

    // Main Stats
    public float Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    public float Agility
    {
        get { return agility; }
        set { agility = value; }
    }
    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public float Vigor
    {
        get { return vigor; }
        set { vigor = value; }
    }
    public float Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    public float Endurance
    {
        get { return endurance; }
        set { endurance = value; }
    }
    public float Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }

    // Derived Stats
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public float Resistance
    {
        get { return (Vitality * 0.2f) + (Vigor * 0.2f) + (Endurance * 0.2f); }
    }
    public override float MoveSpeed
    {
        get { return Agility * 1.5f; }
    }
    public float EvadeDistance
    {
        get { return Agility * 0.2f; }
    }
    public float RawDamage
    {
        get { return Strength * 5.0f; }
    }
    public float Blocking
    {
        get { return Strength * 0.2f; }
    }
    public float ComboDelay
    {
        get { return 1.0f - (Vigor * 0.02f); }
    }
    public float DamageMultiplier
    {
        get { return Vigor * 0.1f; }
    }
    public float EnemyAttackExecutionTime
    {
        get { return 1.0f + (Intelligence * 0.05f); }
    }
    public float RangeMissChance
    {
        get { return 10.0f - (Intelligence * 0.5f); }
    }
    public float MaxSprintMeter
    {
        get { return Endurance * 10.0f; }
    }
    public float CurrentSprintMeter
    {
        get { return currentSprintStamina; }
        set { currentSprintStamina = value; }
    }
    public float MaxBlockMeter
    {
        get { return Endurance * 5.0f; }
    }
    public float CurrentBlockMeter
    {
        get { return currentBlockMeter; }
        set { currentBlockMeter = value; }
    }
    public float Accuracy
    {
        get { return Dexterity * 2.0f; }
    }
    public float AttackInterval
    {
        get { return Dexterity * 0.1f; }
    }
}
