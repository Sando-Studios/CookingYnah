using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientItem : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    public Ingredient associatedIngredient;

    private Coroutine routine;

    private bool isHovering;

    private Vector3 referencePosition;
    
    public Crafting.Slot refSlot;

    private void Start()
    {
        referencePosition = transform.position;
    }

    public void StartFollowMouse()
    {
        routine = StartCoroutine(_followMouse());
        refSlot?.transform.SetAsLastSibling();
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
        StopCoroutine(routine);
        transform.position = referencePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (associatedIngredient.name == "empty") return;
        
        if (!isHovering)
        {
            isHovering = true;
            StartFollowMouse();
            return;
        }

        isHovering = false;
        StopFollowMouse();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Debug.Log($"{eventData.hovered}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var slot = other.gameObject.GetComponent<Crafting.Slot>();
        if (!slot) return;
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // other.gameObject.GetComponent<Crafting.Slot>();
        // Debug.Log("ing");
    }
}
