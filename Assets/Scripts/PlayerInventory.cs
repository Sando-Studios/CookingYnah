using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventory = 20;
    private Dictionary<string, int> itemDictionary = new Dictionary<string, int>();

    public void AddItem(GameObject item)
    {
        if (itemDictionary.ContainsKey(item.name))
        {
            itemDictionary[item.name] += 1;
            Destroy(item);
        }
        else if (itemDictionary.Count <= maxInventory)
        {
            itemDictionary.Add(item.name, 1);
            Destroy(item);
        }
    }
}
