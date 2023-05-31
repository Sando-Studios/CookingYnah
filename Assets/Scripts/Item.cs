using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private DropItemData itemBuffData;

    public string GetItemName()
    {
        return itemName;
    }
    public DropItemData GetItemBuffData()
    {
        return itemBuffData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerInventory i = other.gameObject.GetComponent<Player>().GetInventory();
            i.AddItem(this);
        }
    }
}
