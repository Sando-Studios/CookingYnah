using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crafting
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        private IngredientItem inSlot;
        public uint amount;

        public GameObject emptySlot;

        public Ingredient GetIngredientInSlot()
        {
            return inSlot.associatedIngredient;
        }

        public IngredientItem Replace(IngredientItem newItem)
        {
            // inSlot.StartFollowMouse();
            
            inSlot = newItem;
            newItem.refSlot = this;
            throw new NotImplementedException();
        }

    }
}
