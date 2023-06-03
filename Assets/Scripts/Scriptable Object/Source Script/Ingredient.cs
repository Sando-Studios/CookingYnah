using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string name;
    public GameObject prefab;
}
