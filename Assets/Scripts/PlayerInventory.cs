using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;
using static UnityEditor.Progress;

public struct InventorySlot
{
    public string itemName;
    public int itemQuantity;
}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventory = 20;

    private List<InventorySlot> invetoryList = new List<InventorySlot>();

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemValueText;

    public void AddItem(Item itemToAdd)
    {
        string dropItemName = itemToAdd.GetItemName();

        InventorySlot result = invetoryList.Find(item => item.itemName == dropItemName);

        if (result.itemName != null)
        {
            //Debug.Log("Add Item");
            int i = invetoryList.IndexOf(result);

            InventorySlot s = invetoryList[i];
            s.itemQuantity += 1;

            invetoryList[i] = s;

            Destroy(itemToAdd.gameObject);
        }
        else
        {
            if (invetoryList.Count <= maxInventory)
            {
                //Debug.Log("Add New Item");
                InventorySlot newItem = new InventorySlot
                {
                    itemName = dropItemName,
                    itemQuantity = 1
                };
                invetoryList.Add(newItem);
                Destroy(itemToAdd.gameObject);
            }
            else
            {
                //Debug.Log("Inventory Full");
            }
        }

        UIManager.instance.UpdateInventoryUI();
    }

    public void RemoveItem(string itemToRemove)
    {
        InventorySlot result = invetoryList.Find(item => item.itemName == itemToRemove);

        if (result.itemName != null)
        {
            invetoryList.Remove(result);
        }
        UIManager.instance.UpdateInventoryUI();
    }

    public int GetItemQuantity(string itemToGet)
    {
        InventorySlot result = invetoryList.Find(item => item.itemName == itemToGet);

        if (result.itemName != null)
        {
            return result.itemQuantity;
        }
        //Debug.Log("Item " + itemToGet + " is not in the inventory");
        return 0;
    }

    public void UpdateItemCount()
    {
        InventorySlot result = invetoryList.Find(item => item.itemName == "Drop Item(Clone)");

        itemValueText.text = result.itemQuantity.ToString();
    }

    public List<InventorySlot> GetInventoryList()
    {
        return invetoryList;
    }
}
