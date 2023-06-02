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
    public float AttackRange 
    { 
        get { return attackRange; } 
        set { attackRange = value; }
    }
}
