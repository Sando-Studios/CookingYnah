using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crafting
{
    public class Slot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private IngredientItem inSlot;
        public uint amount;

        public GameObject emptySlot;

        public Ingredient GetIngredientInSlot()
        {
            return inSlot.associatedIngredient;
        }
        
        // Possible actions
        // Put on empty
        // Remove Something
        // Replace

        private IngredientItem Replace(IngredientItem newItem)
        {
            var oldItem = inSlot;
            
            inSlot.StartFollowMouse();
            inSlot.refSlot = null; // Might change when closing the crafting tab (might not despawn or shit)
            newItem.refSlot = this;
            newItem.transform.position = transform.position;
            inSlot = newItem;
            
            return oldItem;
        }

        public void Put(IngredientItem newItem)
        {
            // Put on empty
            if (inSlot.associatedIngredient.name == "empty")
            {
                inSlot.gameObject.SetActive(false);

                newItem.refSlot = this;

                newItem.transform.position = transform.position;

                inSlot = newItem;

                return;
            }
            
            // Not Empty (Replace)

            Replace(newItem);

        }

        public void Remove()
        {
            // inSlot.StartFollowMouse();
            
            inSlot.refSlot = null;
            emptySlot.gameObject.SetActive(true);
            inSlot = emptySlot.GetComponent<IngredientItem>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
