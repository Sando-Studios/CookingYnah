using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private DropItemData itemData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetData(DropItemData data)
    {
        itemData = data;

        itemName = itemData.Name;
        spriteRenderer.sprite = itemData.SpriteToRender;
    }
    public string GetItemName()
    {
        return itemName;
    }
    public DropItemData GetItemBuffData()
    {
        return itemData;
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
