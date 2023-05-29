using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] private string unitName;
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;

    public string UnitName 
    { 
        get { return unitName; } 
        set { unitName = value; }
    }
    public int Health 
    { 
        get { return health; } 
        set { health = value; }
    }
    public float MoveSpeed 
    {
        get { return moveSpeed; } 
        set { moveSpeed = value; }
    }
}

