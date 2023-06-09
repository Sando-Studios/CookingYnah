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
        
        itemName = itemData.Name;
        itemSprite = itemData.SpriteToRender;
        spriteRenderer.sprite = itemSprite;
        
    }
    public string GetItemName()
    {
        return itemName;
    }
    public ItemData GetItemBuffData()
    {
        return itemData;
    }
    public Sprite GetItemSprite()
    {
        return itemSprite;
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
