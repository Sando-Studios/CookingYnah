using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Player player;
    private PlayerUnitData playerData;
    private PlayerInventory playerInventory;
    [Header("Health UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Image hpBarImage;

    [Header("Stamina UI")]
    [SerializeField] private Image staminaBarImage;

    [Header("Character Stats UI")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject characterImage;
    [SerializeField] private TextMeshProUGUI vitText;
    [SerializeField] private TextMeshProUGUI resText;
    [SerializeField] private TextMeshProUGUI strText;

    [SerializeField] private GameObject statPopupToolTip;
    [SerializeField] private TextMeshProUGUI statDescText;
    [SerializeField] private StatDescriptions statDescriptions;
    private bool isOverStat = false;

    [Header("Inventory UI")]
    [SerializeField] private GameObject itemNodePrefab;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSlotParent;
    [SerializeField] private GameObject itemPopupToolTip;
    [SerializeField] private TextMeshProUGUI itemToolTipText;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPermEffectsText;
    [SerializeField] private TextMeshProUGUI itemTempEffectsText;
    private bool isOverItem = false;

    [Header("Crafting UI")]
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject craftingItemParent;
    [SerializeField] private Crafting.Crafter crafter;
    [SerializeField] private GameObject craftingPopup;
    [SerializeField] private CraftingTutorial _craftingTutorial;
    public bool craftingBtnBlock = false;

    private void Start()
    {
        playerData = player.GetPlayerData();
        playerInventory = player.GetInventory();
    }

    public void CraftingStartTutorialSequence()
    {
        _craftingTutorial.StartSequences();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            itemPanel.SetActive(!itemPanel.activeInHierarchy);
            UpdateInventoryUI();
        }
        if (Input.GetButtonDown("Crafting") && player.GetNearStation() && !craftingBtnBlock)
        {
            ToggleCraftingPanel();
        }

        if (!player.GetNearStation()) { craftingPanel.SetActive(false); }

        if (Input.GetButtonDown("Stats"))
        {
            statsPanel.SetActive(!statsPanel.activeInHierarchy);
            characterImage.SetActive(!characterImage.activeInHierarchy);
            UpdateStatsUI();
        }

        if (isOverStat) { statPopupToolTip.transform.position = Input.mousePosition; }

        if (isOverItem) { itemPopupToolTip.transform.position = Input.mousePosition; }
    }

    public void UpdateHpUI()
    {
        //hpText.text = playerData.CurrentHealth.ToString() + " / " + playerData.MaxHealth.ToString();
    }

    public void UpdateHpBarUI()
    {
        float currentHP = playerData.CurrentHealth;
        float maxHealth = playerData.MaxHealth;

        float normalized = currentHP / maxHealth;

        hpBarImage.fillAmount = normalized;
    }

    public void UpdateStaminaBarUI()
    {
        float currentHP = playerData.CurrentStamina;
        float maxHealth = playerData.MaxStamina;

        float normalized = currentHP / maxHealth;

        staminaBarImage.fillAmount = normalized;
    }
    public void UpdateStatsUI()
    {
        vitText.text = playerData.Vitality.ToString();
        resText.text = playerData.Resilience.ToString();
        strText.text = playerData.Strength.ToString();
    }

    public void ToggleCraftingPanel()
    {
        craftingPanel.SetActive(!craftingPanel.activeInHierarchy);
        UpdateCraftingInventoryUI();
    }

    public void ForceOpenCraftingPanel()
    {
        craftingPanel.SetActive(true);
        UpdateCraftingInventoryUI();
    }

    public void ForceCloseCraftingPanel()
    {
        craftingPanel.SetActive(false);
        UpdateCraftingInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        DestroyChildren(itemSlotParent.transform);

        List<InventorySlot> list = playerInventory.GetInventoryList();
        foreach (InventorySlot slot in list)
        {
            GameObject clone = Instantiate(itemNodePrefab, transform);
            clone.transform.SetParent(itemSlotParent.transform);
            clone.transform.SetAsLastSibling();
            clone.GetComponent<InventoryNode>().SetData(slot.itemName, slot.itemQuantity, slot.itemSprite);
        }

        UpdateContentHeight(itemSlotParent.transform.childCount, itemSlotParent);
    }

    public void UpdateCraftingInventoryUI()
    {
        // Out with the old
        DestroyChildren(craftingItemParent.transform);

        // In with the new
        var rawList = playerInventory.GetInventoryList();

        foreach (var itemStruct in rawList)
        {
            var o = crafter.SpawnInventorySlot(itemStruct.itemName, itemStruct.itemQuantity, itemStruct.itemSprite);

            o.gameObject.name = $"{itemStruct.itemName}";
        }

        UpdateContentHeight(rawList.Count, craftingItemParent);
    }

    private void DestroyChildren(Transform target)
    {
        var count = target.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(target.GetChild(i).gameObject);
        }
    }

    private void UpdateContentHeight(int itemCount, GameObject targetParent)
    {
        RectTransform contentRectTransform = targetParent.transform.GetComponent<RectTransform>();
        float newHeight = CalculateContentHeight(itemCount);
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, newHeight);
    }

    private float CalculateContentHeight(int itemCount)
    {
        int additionalHeight = (itemCount / 4) * 50;
        return 60 + additionalHeight;
    }

    public void OnCursorOverStat(TargetStat targetStat)
    {
        var s = targetStat;

        switch (s)
        {
            case TargetStat.VitStat:
                statDescText.text = statDescriptions.Vitality;
                break;
            case TargetStat.ResStat:
                statDescText.text = statDescriptions.Resilience;
                break;
            case TargetStat.StrStat:
                statDescText.text = statDescriptions.Strength;
                break;
            default:
                Debug.Log("Unknown Stat");
                break;
        }

        isOverStat = true;
        statPopupToolTip.SetActive(true);
    }

    public void OnCursorOffStat()
    {
        isOverStat = false;
        statPopupToolTip.SetActive(false);
    }

    public void OnCursorOverItem(string name)
    {
        List<InventorySlot> list = playerInventory.GetInventoryList();

        InventorySlot result = list.Find(item => item.itemName == name);

        itemToolTipText.text = result.itemName;
        itemImage.sprite = result.itemSprite;
        itemNameText.text = result.itemName;

        string stringToAdd1 = "";
        string stringToAdd2 = ""; ;

        itemPermEffectsText.text = "";
        itemTempEffectsText.text = "";

        ItemData itemData = result.itemBuffData;

        foreach (KeyValuePair<TargetStat, int> pair in itemData.PermanentBuffs)
        {
            var s = pair.Key;

            switch (s)
            {
                case TargetStat.VitStat:
                    stringToAdd1 += "+" + pair.Value + " VIT<br>";
                    break;
                case TargetStat.StrStat:
                    stringToAdd1 += "+" + pair.Value + " STR<br>";
                    break;
                case TargetStat.ResStat:
                    stringToAdd1 += "+" + pair.Value + " RES<br>";
                    break;
            }
        }

        foreach (KeyValuePair<TargetStat, int> pair in itemData.TemporaryBuffs)
        {
            var s = pair.Key;

            switch (s)
            {
                case TargetStat.VitStat:
                    stringToAdd2 += "+" + pair.Value + " VIT<br>";
                    break;
                case TargetStat.StrStat:
                    stringToAdd2 += "+" + pair.Value + " STR<br>";
                    break;
                case TargetStat.ResStat:
                    stringToAdd2 += "+" + pair.Value + " RES<br>";
                    break;
            }
        }

        itemPermEffectsText.text += stringToAdd1;
        itemTempEffectsText.text += stringToAdd2;

        isOverItem = true;
        itemPopupToolTip.SetActive(true);
    }

    public void OnCursorOffItem()
    {
        isOverItem = false;
        itemPopupToolTip.SetActive(false);
    }

    public void SetCraftingPopUp()
    {
        craftingPopup.SetActive(player.GetNearStation());
    }
}
