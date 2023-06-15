using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asyncoroutine;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

namespace Crafting
{
    public class Crafter : MonoBehaviour
    {
        public SerializedDictionary<string, Recipe> recipes = new();

        public Slot[] slots;
        
        public Slot output;

        [SerializeField] private GameObject craftingSlotPrefab;

        [SerializeField] private Transform craftingSectionParent;

        [SerializeField] private PlayerInventory inventory;

        [SerializeField] private Transform craftingInventorySection;

        private Dictionary<string, IngredientItem> cache = new();
        
        [Header("UI")]

        [SerializeField]
        [Tooltip("Success Panel")]
        private SuccessPanel correctCraftSprite;
        [SerializeField]
        [Tooltip("The Pot")]
        private Image wrongCraftSprite;

        [SerializeField] private Sprite wrongSprite;
        
        public void Listen()
        {
            Debug.Log(GetOutput().itemName != "None");
        }

        public void CraftToOutput()
        {
            var ing = GetOutput();

            if (ing.itemName == InventorySlot.Empty.itemName)
            {
                ChangeOutput();
                return;
            }

            ChangeOutput(ing);

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
            
            return InventorySlot.Empty;
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

        private async void ChangeOutput()
        {
            var old = wrongCraftSprite.sprite;
            wrongCraftSprite.sprite = wrongSprite;
            await new WaitForSeconds(1f);
            wrongCraftSprite.sprite = old;
        }

        private async void ChangeOutput(InventorySlot item)
        {
            correctCraftSprite.Show(item.itemSprite, item.itemName, "NaN");
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
