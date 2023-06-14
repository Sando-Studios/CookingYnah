using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class CrafterInventory : Crafting.Slot
{
    public GameObject ogInvPanel;
    
    [SerializeField] private PlayerInventory originalInventory;

    // private bool Exists(IngredientItem)
    // {
    //     originalInventory.GetInventoryList().Find()
    // }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IngredientItem item))
        {
            item.transform.SetParent(transform);
            originalInventory.AddItem(item.Name, item.ItemData);
        }
    }

    private async void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IngredientItem item))
        {
            originalInventory.RemoveItem(item.Name);
            await new WaitForEndOfFrame();
            // Will probs go to main canvas obj idk man
            if (item != null)
                item.transform.SetParent(transform.parent);
            
        }
    }

    public override void Put(IngredientItem newItem)
    {
        newItem.transform.SetParent(transform);
        
    }

    public override void Remove()
    {
        // inSlot.refSlot = null;
    }

    private void OnEnable()
    {
        ogInvPanel.SetActive(false);
    }

    private void OnDisable()
    {
        ogInvPanel.SetActive(true);
    }
}
