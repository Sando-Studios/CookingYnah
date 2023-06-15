using System;
using System.Collections;
using System.Collections.Generic;
using Crafting;
using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "SampleRecipe")]
    [Serializable]
    public class Recipe : ScriptableObject
    {
        public string[] slots;

        public InventorySlot output;
    }
}