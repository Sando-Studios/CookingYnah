using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryNode : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private Button useButton;
    private string itemName;

    [SerializeField] private GameObject craftingItemEquivalent;

    public static event Action<string> OnUseItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        useButton.onClick.AddListener(OnButtonClick);
    }
    private void OnDisable()
    {
        useButton.onClick.RemoveAllListeners();
    }

    void OnButtonClick()
    {
        OnUseItem?.Invoke(itemName);
    }

    public void SetData(string itemID, int itemQuantity, Sprite itemSprite)
    {
        itemNameText.text = itemID;
        itemName = itemID;
        itemQuantityText.text = itemQuantity.ToString();
        itemIconImage.sprite = itemSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // var item = Instantiate(craftingItemEquivalent, eventData.position, Quaternion.identity).GetComponent<IngredientItem>();
        // item.StartFollowMouse();
    }
}
