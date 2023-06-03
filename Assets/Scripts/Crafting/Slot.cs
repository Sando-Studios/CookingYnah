using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crafting
{
    public class Slot : MonoBehaviour, IPointerUpHandler
    {
        public IngredientItem inSlot;
        public uint amount;

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
