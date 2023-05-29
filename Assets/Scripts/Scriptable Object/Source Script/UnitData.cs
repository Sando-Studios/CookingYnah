using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] private string unitName;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float moveSpeed;

    public string UnitName 
    { 
        get { return unitName; } 
        set { unitName = value; }
    }
    public int MaxHealth 
    { 
        get { return maxHealth; } 
        set { maxHealth = value; }
    }
    public int CurrentHealth 
    { 
        get { return currentHealth; } 
        set { currentHealth = value; }
    }
    public float MoveSpeed 
    {
        get { return moveSpeed; } 
        set { moveSpeed = value; }
    }
}

