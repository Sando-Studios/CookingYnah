using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] private string unitName;
    private int currentHealth;
    

    public string UnitName 
    { 
        get { return unitName; } 
        set { unitName = value; }
    }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}

