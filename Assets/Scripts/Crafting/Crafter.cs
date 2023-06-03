using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Profiling;

namespace Crafting
{
    public class Crafter : MonoBehaviour
    {
        public SerializedDictionary<string, Recipe> recipes = new();

        public Slot[] slots;

        public Slot output;

        public void Listen()
        {
            Debug.Log(CheckIfValid());
            Debug.Log(GetHashCode());
        }

        public bool CheckIfValid()
        {
            var inSlots = new List<CraftingSlot>(4);

            for (int i = 0; i < 4; i++)
            {
                var s = slots[i];

                if (s == null)
                {
                    Debug.Log("whhat");
                }
                
                inSlots.Add(new CraftingSlot()
                {
                    amount = s.amount,
                    item = s.inSlot.associatedIngredient,
                });
            }

            // Checking 
            foreach (var v in inSlots)
            {
                Debug.Log($"inslo {v.item}");
            }

            foreach (var (name, recipe) in recipes)
            {
                // Checking
                foreach (var v in recipe.slots)
                {
                    Debug.Log($"rec {v.item}");
                }
                if (CompareSlots(recipe.slots, inSlots.ToArray()))
                {
                    return true;
                }
            }

            return false;
        }

        private  bool CompareSlots(CraftingSlot[] a, CraftingSlot[] b)
        {
            if (a == b) return true;
            
            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length; i++)
            {
                var aa = a[i];
                var bb = b[i];

                if (aa != bb) return false;
            }

            return true;
        }
    }

    [Serializable]
    public struct CraftingSlot
    {
        [Range(0, 64)] public uint amount;
        public Ingredient item;

        public override bool Equals(object obj)
        {
            if (obj is not CraftingSlot) return false;

            return item.name == ((CraftingSlot)obj).item.name;
        }

        public static bool operator ==(CraftingSlot a, CraftingSlot b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(CraftingSlot a, CraftingSlot b)
        {
            return !a.Equals(b);
        }
    }
}