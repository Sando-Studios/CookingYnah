using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public struct InventorySlot
{
    private static readonly InventorySlot empty = new InventorySlot()
    {
        itemName = "None",
        itemQuantity = 0,
        itemSprite = null,
        itemBuffData = null,
    };

    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;
    public ItemData itemBuffData;

    public InventorySlot (ItemData data)
    {
        itemName = data.Name;
        itemQuantity = 1;
        itemSprite = data.SpriteToRender;
        itemBuffData = data;
    }

    public static InventorySlot Empty
    {
        get => empty;
    }
}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventory = 20;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private List<InventorySlot> inventoryList = new List<InventorySlot>();

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
        if (inventoryList.Count >= maxInventory) return;
        AddItem(item.GetItemName(), item.GetItemBuffData());
        Destroy(item.gameObject);
    }

    public void AddItem(string name, ItemData itemData)
    {
        string dropItemName = name;

        InventorySlot result = inventoryList.Find(item => item.itemName == dropItemName);

        if (result.itemName != null)
        {
            //Debug.Log("Add Item");
            int i = inventoryList.IndexOf(result);

            InventorySlot s = inventoryList[i];
            s.itemQuantity += 1;

            inventoryList[i] = s;

            // Destroy(itemToAdd.gameObject);
        }
        else
        {
            if (inventoryList.Count <= maxInventory)
            {
                //Debug.Log("Add New Item");
                InventorySlot newItem = new InventorySlot
                {
                    itemName = dropItemName,
                    itemQuantity = 1,
                    itemBuffData = itemData,
                    itemSprite = itemData.SpriteToRender,
                };
                inventoryList.Add(newItem);
                // Destroy(itemToAdd.gameObject);
            }
            else
            {
                //Debug.Log("Inventory Full");
            }
        }

        //UIManager.instance.UpdateInventoryUI();
    }

    public void RemoveItem(string itemToRemove)
    {
        InventorySlot result = inventoryList.Find(item => item.itemName == itemToRemove);

        if (result.itemName != null)
        {
            if (result.itemQuantity > 1)
            {
                int i = inventoryList.IndexOf(result);

                InventorySlot s = inventoryList[i];
                s.itemQuantity -= 1;

                inventoryList[i] = s;
            }
            else
            {
                inventoryList.Remove(result);
            }

        }
        //UIManager.instance.UpdateInventoryUI();
    }

    public int GetItemQuantity(string itemToGet)
    {
        InventorySlot result = inventoryList.Find(item => item.itemName == itemToGet);

        if (result.itemName != null)
        {
            return result.itemQuantity;
        }
        //Debug.Log("Item " + itemToGet + " is not in the inventory");
        return 0;
    }

    public void UseItem(string name)
    {
        InventorySlot result = inventoryList.Find(item => item.itemName == name);

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

        BuffManager.instance.ApplyHeal(result.itemBuffData.HealAmount);
        RemoveItem(result.itemName);

        audioSource.Stop();
        audioSource.Play();

        UIManager.instance.UpdateInventoryUI();
    }

    public List<InventorySlot> GetInventoryList()
    {
        return inventoryList;
    }
}
