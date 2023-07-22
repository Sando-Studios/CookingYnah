using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private ItemData itemData;
    private Sprite itemSprite; 
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        DelayItemPickup();
    }

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
        if(other.tag == "Player" && !other.isTrigger)
        {
            PlayerInventory i = other.gameObject.GetComponent<Player>().GetInventory();
            i.AddItem(this);
        }
    }

    private async void DelayItemPickup()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;

        await new WaitForSeconds(2f);

        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}
