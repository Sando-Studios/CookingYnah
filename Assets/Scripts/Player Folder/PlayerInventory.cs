using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;

using UnityEngine.Rendering;

public struct InventorySlot
{
    public string itemName;
    public int itemQuantity;
    public DropItemData itemBuffData;
}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventory = 20;

    private List<InventorySlot> invetoryList = new List<InventorySlot>();

    private void OnEnable()
    {
        InventoryNode.OnUseItem += UseItem;
    }
    private void OnDisable()
    {
        InventoryNode.OnUseItem -= UseItem;
    }

    public void AddItem(Item item)
    {
        if (invetoryList.Count >= maxInventory) return;
        AddItem(item.name, item.GetItemBuffData());
        Destroy(item.gameObject);
    }

    public void AddItem(string name, DropItemData itemData)
    {
        string dropItemName = name;

        InventorySlot result = invetoryList.Find(item => item.itemName == dropItemName);

        if (result.itemName != null)
        {
            //Debug.Log("Add Item");
            int i = invetoryList.IndexOf(result);

            InventorySlot s = invetoryList[i];
            s.itemQuantity += 1;

            invetoryList[i] = s;

            // Destroy(itemToAdd.gameObject);
        }
        else
        {
            if (invetoryList.Count <= maxInventory)
            {
                //Debug.Log("Add New Item");
                InventorySlot newItem = new InventorySlot
                {
                    itemName = dropItemName,
                    itemQuantity = 1,
                    itemBuffData = itemData,
                };
                invetoryList.Add(newItem);
                // Destroy(itemToAdd.gameObject);
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
            if (result.itemQuantity > 1)
            {
                int i = invetoryList.IndexOf(result);

                InventorySlot s = invetoryList[i];
                s.itemQuantity -= 1;

                invetoryList[i] = s;
            }
            else
            {
                invetoryList.Remove(result);
            }

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

    public void UseItem(string name)
    {
        InventorySlot result = invetoryList.Find(item => item.itemName == name);

        Dictionary<TargetStat, int> permaBuffs = result.itemBuffData.PermanentBuffs;
        Dictionary<TargetStat, int> tempBuffs = result.itemBuffData.TemporaryBuffs;

        if (permaBuffs.Count > 0)
        {
            foreach (var item in permaBuffs)
            {
                BuffManager.instance.ApplyBuff(item.Key, item.Value);
            }
        }

        if (tempBuffs.Count > 0)
        {
            foreach (var item in tempBuffs)
            {
                BuffManager.instance.ApplyTempBuffs(item.Key, item.Value, result.itemBuffData.Duration);
            }
        }

        RemoveItem(result.itemName);
    }

    public List<InventorySlot> GetInventoryList()
    {
        return invetoryList;
    }
}
