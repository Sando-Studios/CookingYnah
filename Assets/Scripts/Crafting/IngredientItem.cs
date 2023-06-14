using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IngredientItem : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    public Ingredient associatedIngredient;

    private Coroutine routine;

    private bool isHovering;

    private Vector3 referencePosition;
    
    public Crafting.Slot refSlot;

    private Crafting.Slot focusedSlot;

    [SerializeField] private ItemData dropItemData;

    public string Name
    {
        get => dropItemData.Name;
    }

    public ItemData ItemData
    {
        get => dropItemData;
    }

    private async void Start()
    {
        
    }

    public async void SetRefPosition()
    {
        // if (transform.parent.TryGetComponent(out GridLayout layout))
        // {
        //     referencePosition = layout.CellToWorld(layout.WorldToCell(transform.position));
        // }
        await new WaitForEndOfFrame();
        referencePosition = transform.position;
    }

    public void StartFollowMouse()
    {
        isHovering = true;
        
        if (routine != null) return;
        
        routine = StartCoroutine(_followMouse());
        // refSlot
        // refSlot?.transform.SetAsLastSibling();
        transform.SetAsLastSibling();
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
        transform.position = referencePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (associatedIngredient.name == "empty") return;
        
        if (!isHovering)
        {
            refSlot?.Remove();
            StartFollowMouse();
            return;
        }
        
        StopFollowMouse();
        focusedSlot?.Put(this);
        
        if (focusedSlot is CrafterInventory)
        {
            SetRefPosition();
        }
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

    private void OnDisable()
    {
        if (associatedIngredient.name == "empty") return;
        
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // var slot = other.gameObject.GetComponent<Crafting.Slot>();
        // if (!slot) return;
        //
        // focusedSlot = slot;
    }
}
