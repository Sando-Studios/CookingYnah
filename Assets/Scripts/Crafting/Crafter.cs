using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asyncoroutine;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Crafting
{
    public class Crafter : MonoBehaviour
    {
        public SerializedDictionary<string, Recipe> recipes = new();

        public Slot[] slots;

        // TODO: Make preview
        [SerializeField] private GameObject correctCraftSprite;
        [SerializeField] private GameObject wrongCraftSprite;
        
        public Slot output;

        [SerializeField] private GameObject craftingSlotPrefab;

        [SerializeField] private Transform craftingSectionParent;

        [SerializeField] private PlayerInventory inventory;

        [SerializeField] private Transform craftingInventorySection;

        private Dictionary<string, IngredientItem> cache = new();

        public void Listen()
        {
            Debug.Log(GetOutput().itemName != "None");
        }

        public void CraftToOutput()
        {
            var ing = GetOutput();

            if (ing.itemName == "None")
            {
                ChangeOutput(false);
                return;
            }

            ChangeOutput(true);

            RemoveFromInventoryFromSlots();
            ClearSlots();
            
            inventory.AddItem(ing.itemName, ing.itemBuffData);
            
            UIManager.instance.UpdateCraftingInventoryUI();
        }

        public InventorySlot GetOutput()
        {
            var inSlots = new List<string>(4);

            for (int i = 0; i < 4; i++)
            {
                var s = slots[i];

                if (s == null)
                {
                    Debug.Log("what");
                }
                
                inSlots.Add(s.GetIngredientInSlot());
            }

            foreach (var (name, recipe) in recipes)
            {
                if (CompareSlots(recipe.slots.Select(data =>
                    {
                        if (data == null) return "None";
                        return data.Name;
                    }).ToArray(), inSlots.ToArray()))
                {
                    return new InventorySlot(recipe.output);
                }
            }
            
            return new InventorySlot()
            {
                itemName = "None"
            };
        }

        private static bool CompareSlots(string[] a, string[] b)
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

        private void ClearSlots()
        {
            foreach (var slot in slots)
            {
                slot.Clear();
            }
        }

        private void RemoveFromInventoryFromSlots()
        {
            foreach (var slot in slots)
            {
                if (slot.GetIngredientInSlot() == "None") continue;
                inventory.RemoveItem(slot.GetIngredientInSlot());
            }
        }

        public IngredientItem SpawnInventorySlot(string name, int amount, Sprite img)
        {
            var o = Instantiate(craftingSlotPrefab).GetComponent<IngredientItem>();

            o.SetData(name, amount, img);
            o.craftingSectionParent = craftingSectionParent;

            o.crafter = this;

            o.InventoryAddCloneEvent += AddQuantityToItem;

            o.baseParent = craftingInventorySection;
            o.transform.SetParent(craftingInventorySection);
            return o;
        }

        private async void ChangeOutput(bool isGood)
        {
            throw new NotImplementedException("No sprites yet");
            await new WaitForSeconds(1.5f);
        }

        public void AddQuantityToItem(IngredientItem clone, string name, int amount)
        {
            // Changing Values
            if (cache.TryGetValue(clone.itemName, out var item))
            {
                item.UpdateAmount(item.amount + 1);
                
                Destroy(clone.gameObject);
            }
        }

        public void CacheItemsToDict()
        {
            // Clears old cache
            cache = new();
            
            var cCount = craftingInventorySection.transform.childCount;
            for (int i = 0; i < cCount; i++)
            {
                var child = craftingInventorySection.transform.GetChild(i);

                var ingItem = child.GetComponent<IngredientItem>();
                
                cache.TryAdd(ingItem.itemName, ingItem);
            }
        }
        
    }
}
