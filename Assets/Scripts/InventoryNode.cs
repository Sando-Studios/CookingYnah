using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private Button useButton;
    private string itemName;

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

    public void SetData(string itemID, int itemQuantity)
    {
        itemNameText.text = itemID;
        itemName = itemID;
        itemQuantityText.text = itemQuantity.ToString();
    }
}
