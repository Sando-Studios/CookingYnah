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
        protected IngredientItem inSlot;
        public uint amount;

        public string GetIngredientInSlot()
        {
            return inSlot == null ? "None" : inSlot.itemName;
        }

        public void Clear()
        {
            if (inSlot == null) return;
            Destroy(inSlot.gameObject);
            inSlot = null;
        }

        // Possible actions
        // Put on empty
        // Replace
        // Remove Something

        private IngredientItem Replace(IngredientItem newItem)
        {
            var oldItem = inSlot;

            newItem.refSlot = this;
            newItem.transform.position = transform.position;

            oldItem.PutOnCraftingSection();
            oldItem.StartFollowMouse();
            oldItem.refSlot = null;
            
            newItem.PutOnCraftingSection(); // Force

            oldItem.transform.SetAsLastSibling();
            inSlot = newItem;
            
            return oldItem;
        }

        public virtual void Put(IngredientItem newItem)
        {
            // Put on empty
            if (inSlot == null)
            {
                newItem.refSlot = this;

                newItem.transform.position = transform.position;

                inSlot = newItem;
                
                newItem.PutOnCraftingSection();

                return;
            }
            
            // Not Empty (Replace)
            Replace(newItem);

        }

        public virtual void Remove()
        {
            inSlot.StartFollowMouse();
            
            inSlot.refSlot = null;
            inSlot = null;
        }

        private void OnDisable()
        {
            Clear();
        }
    }
}
