using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientItem : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    private Coroutine routine;

    private bool isHovering;

    public Crafting.Slot refSlot;

    private Crafting.Slot focusedSlot;

    public TextMeshProUGUI quantityText;
    public Image img;

    public string itemName;
    public int amount;

    public Transform baseParent;
    public Transform craftingSectionParent;

    public Action<IngredientItem, string, int> InventoryAddCloneEvent;
    public Crafting.Crafter crafter;

    private void Start()
    {
        this.transform.localScale = Vector3.one;
    }

    public void SetData(string name, int amount, Sprite img)
    {
        itemName = name;
        this.amount = amount;

        quantityText.text = $"{amount}";
        
        this.img.sprite = img;
    }

    public void UpdateAmount(int newAmount)
    {
        amount = newAmount;

        quantityText.text = $"{amount}";
    }

    public void StartFollowMouse()
    {
        isHovering = true;
        
        if (routine != null) return;
        
        routine = StartCoroutine(_followMouse());
    }

    private IEnumerator _followMouse()
    {
        for (;;)
        {
            yield return new WaitForEndOfFrame();
            gameObject.transform.position = Input.mousePosition;
        }
    }

    public void StopFollowMouse()
    {
        isHovering = false;
        StopCoroutine(routine);
        routine = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (amount > 1)
        {
            // Debug.Log($"{itemName} is more than 1");
            var o = SpawnSingleCopy();
            UpdateAmount(--amount);
            o.OnPointerClick(new PointerEventData(EventSystem.current));
            return;
        }
        
        if (!isHovering)
        {
            refSlot?.Remove();
            if (refSlot == null && focusedSlot == null) // Newly spawned
                PutOnCraftingSection();
            StartFollowMouse();
            return;
        }
        
        // Hovering Segment
        
        if (refSlot == null)
            PutOnInventorySection();
        StopFollowMouse();
        
        if (focusedSlot != null)
        {
            focusedSlot.Put(this);
            return;
        }
        
        // If the item is moved to inv section
        InventoryAddCloneEvent?.Invoke(this, itemName, amount);
    }

    public void PutOnCraftingSection()
    {
        transform.SetParent(craftingSectionParent);
    }

    public void PutOnInventorySection()
    {
        crafter.CacheItemsToDict();
        transform.SetParent(baseParent);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Debug.Log($"{eventData.hovered}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var slot = other.gameObject.GetComponent<Crafting.Slot>();
        if (!slot) return;

        focusedSlot = slot;
    }

    private IngredientItem SpawnSingleCopy()
    {
        // Part from Crafter.cs
        var o = Instantiate(gameObject).GetComponent<IngredientItem>();
        
        o.SetData(itemName, 1, img.sprite);
        o.craftingSectionParent = this.craftingSectionParent;

        // Part from UIManager.cs
        o.baseParent = this.baseParent;
        o.transform.SetParent(craftingSectionParent.transform);

        o.gameObject.name = $"{itemName} clone";

        #region Rects

        var rect = o.GetComponent<RectTransform>();
        var selfRect = GetComponent<RectTransform>().rect;

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, selfRect.width);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, selfRect.height);
        
        #endregion

        o.crafter = this.crafter;
        o.InventoryAddCloneEvent += crafter.AddQuantityToItem;

        return o;
    }

    // Might replace the destroy child of in UIManager
    private void OnDisable()
    {
        // Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out CraftingSection section))
        {
            focusedSlot = null;
        }
    }
}
