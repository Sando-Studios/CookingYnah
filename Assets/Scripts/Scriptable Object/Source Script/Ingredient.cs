using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string name;
    
    [Tooltip("")]
    public GameObject prefab;
}
