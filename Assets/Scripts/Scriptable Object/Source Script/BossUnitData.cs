using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMajorEnemyUnit", menuName = "Unit/Enemy/MajorEnemy")]
public class BossUnitData : UnitData
{
    [SerializeField] private int maxHealth;

    [SerializeField] private int basicAttackDamage;
    [SerializeField] private int specialAttackDamage;

    [Header("Speeds")]
    [SerializeField] private float walkSpeed;
    [Tooltip("Run Speed is how much is ADDED onto Walk Speed")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float basicAttackSpeed;
    [SerializeField] private float specialAttackSpeed;
    [Tooltip("Stunned Duration in SECONDS")]
    [SerializeField] private float stunnedDuration;

    [Header("Range")]
    [SerializeField] private float attackRange;

    [Header("Artifact")]
    [SerializeField] private Artifacts artifact;

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int BasicAttackDamage
    {
        get { return basicAttackDamage; }
    }

    public int SpecialAttackDamage
    {
        get { return specialAttackDamage; }
    }

    public float WalkSpeed
    {
        get { return walkSpeed; }
    }

    public float RunSpeed
    {
        get { return runSpeed; }
    }

    public float BasicAttackSpeed
    {
        get { return basicAttackSpeed; }
    }
    public float SpecialAttackSpeed
    {
        get { return specialAttackSpeed; }
    }
    public float StunnedDuration
    {
        get { return stunnedDuration; }
    }
    public float AttackRange
    {
        get { return attackRange; }
    }
    public Artifacts Artifact
    {
        get { return artifact; }
    }

    public void SetHealthToDefault()
    {
        CurrentHealth = maxHealth;
    }
}
