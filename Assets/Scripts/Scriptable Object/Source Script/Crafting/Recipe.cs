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
        [Tooltip("Proteins|Cards|Vegetables     Proteins|Cards|Vegetables")]
        public ItemData[] slots = new ItemData[6];

        public ItemData output;
    }
}