using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;

public struct InventorySlot
{
    public string itemName;
    public int itemNum;
}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventory = 20;
    private Dictionary<string, int> itemDictionary = new Dictionary<string, int>();

    private List<InventorySlot> invetorySlot = new List<InventorySlot>();

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemValueText;

    public void AddItem(GameObject dropItem)
    {
        InventorySlot result = invetorySlot.Find(item => item.itemName == dropItem.name);

        if(result.itemName == null && invetorySlot.Count <= maxInventory) 
        {
            invetorySlot.Add(new InventorySlot {itemName = dropItem.name, itemNum = 1});
            Destroy(dropItem);
        }
        else if (result.itemName != null && invetorySlot.Count <= maxInventory)
        {
            result.itemNum += 1;
            Destroy(dropItem);
        }

        UpdateItemCount();
    }

    public void RemoveItem(string itemToRemove)
    {
        InventorySlot result = invetorySlot.Find(item => item.itemName == itemToRemove);

        if (result.itemName != null)
        {
            invetorySlot.Remove(result);
        }
        UpdateItemCount();
    }

    public void UpdateItemCount()
    {
        itemNameText.enabled = true;
        itemValueText.enabled = true;

        InventorySlot result = invetorySlot.Find(item => item.itemName == "Drop Item(Clone)");

        itemValueText.text = result.itemNum.ToString();
    }


}
