using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private ItemData itemData;
    private Sprite itemSprite; 
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetData(ItemData data)
    {
        itemData = data;
        itemSprite = itemData.SpriteToRender;
        itemName = itemData.Name;
        spriteRenderer.sprite = itemData.SpriteToRender;
    }
    public string GetItemName()
    {
        return itemName;
    }
    public ItemData GetItemBuffData()
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
