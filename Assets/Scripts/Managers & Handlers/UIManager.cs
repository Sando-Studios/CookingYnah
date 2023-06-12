using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
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
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("Character Stats UI")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject characterImage;
    [SerializeField] private TextMeshProUGUI vitText;
    [SerializeField] private TextMeshProUGUI agiText;
    [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private TextMeshProUGUI vigText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI dexText;
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


    private void Start()
    {
        playerData = player.GetPlayerData();
        playerInventory = player.GetInventory();

        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            itemPanel.SetActive(!itemPanel.activeInHierarchy);
            UpdateInventoryUI();
        }
        if (Input.GetButtonDown("Crafting"))
        {

        }
        if (Input.GetButtonDown("Stats"))
        {
            statsPanel.SetActive(!statsPanel.activeInHierarchy);
            characterImage.SetActive(!characterImage.activeInHierarchy);
            UpdateStatsUI();
        }

        if (isOverStat)
        {
            statPopupToolTip.transform.position = Input.mousePosition;
        }
        if (isOverItem)
        {
            itemPopupToolTip.transform.position = Input.mousePosition;
        }
    }

    public void UpdateHpUI()
    {
        hpText.text = playerData.CurrentHealth.ToString() + " / " + playerData.MaxHealth.ToString();
    }
    public void UpdateStatsUI()
    {
        vitText.text = playerData.Vitality.ToString();
        agiText.text = playerData.Agility.ToString();
        strText.text = playerData.Strength.ToString();
        vigText.text = playerData.Vigor.ToString();
        intText.text = playerData.Intelligence.ToString();
        endText.text = playerData.Endurance.ToString();
        dexText.text = playerData.Dexterity.ToString();
    }

    public void UpdateInventoryUI()
    {
        
        List<InventorySlot> list = playerInventory.GetInventoryList();

        if (itemSlotParent.transform.childCount > 0)
        {
            for (int i = 0; i < itemSlotParent.transform.childCount; i++)
            {
                Destroy(itemSlotParent.transform.GetChild(i).gameObject);
            }
        }

        foreach (InventorySlot slot in list)
        {
            GameObject clone = Instantiate(itemNodePrefab, transform);
            clone.transform.SetParent(itemSlotParent.transform);
            clone.transform.SetAsLastSibling();
            clone.GetComponent<InventoryNode>().SetData(slot.itemName, slot.itemQuantity, slot.itemSprite);
        }

        UpdateContentHeight(itemSlotParent.transform.childCount);
    }
    private void UpdateContentHeight(int itemCount)
    {
        RectTransform contentRectTransform = itemSlotParent.transform.GetComponent<RectTransform>();
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
            case TargetStat.AgiStat:
                statDescText.text = statDescriptions.Agility;
                break;
            case TargetStat.StrStat:
                statDescText.text = statDescriptions.Strength;
                break;
            case TargetStat.VigStat:
                statDescText.text = statDescriptions.Vigor;
                break;
            case TargetStat.IntStat:
                statDescText.text = statDescriptions.Intelligence;
                break;
            case TargetStat.EndStat:
                statDescText.text = statDescriptions.Endurance;
                break;
            case TargetStat.DexStat:
                statDescText.text = statDescriptions.Dexterity;
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
        
        foreach(KeyValuePair<TargetStat, int> pair in itemData.PermanentBuffs)
        {
            var s = pair.Key;

            switch (s)
            {
                case TargetStat.VitStat:
                    stringToAdd1 += "+" + pair.Value + " Vitality<br>";
                    break;
                case TargetStat.AgiStat:
                    stringToAdd1 += "+" + pair.Value + " Agility<br>";
                    break;
                case TargetStat.StrStat:
                    stringToAdd1 += "+" + pair.Value + " Strength<br>";
                    break;
                case TargetStat.VigStat:
                    stringToAdd1 += "+" + pair.Value + " Vigor<br>";
                    break;
                case TargetStat.IntStat:
                    stringToAdd1 += "+" + pair.Value + " Intelligence<br>";
                    break;
                case TargetStat.EndStat:
                    stringToAdd1 += "+" + pair.Value + " Endurance<br>";
                    break;
                case TargetStat.DexStat:
                    stringToAdd1 += "+" + pair.Value + " Dexterity<br>";
                    break;
            }
        }

        foreach(KeyValuePair<TargetStat, int> pair in itemData.TemporaryBuffs)
        {
            var s = pair.Key;

            switch (s)
            {
                case TargetStat.VitStat:
                    stringToAdd2 += "+" + pair.Value + " Vitality<br>";
                    break;
                case TargetStat.AgiStat:
                    stringToAdd2 += "+" + pair.Value + " Agility<br>";
                    break;
                case TargetStat.StrStat:
                    stringToAdd2 += "+" + pair.Value + " Strength<br>";
                    break;
                case TargetStat.VigStat:
                    stringToAdd2 += "+" + pair.Value + " Vigor<br>";
                    break;
                case TargetStat.IntStat:
                    stringToAdd2 += "+" + pair.Value + " Intelligence<br>";
                    break;
                case TargetStat.EndStat:
                    stringToAdd2 += "+" + pair.Value + " Endurance<br>";
                    break;
                case TargetStat.DexStat:
                    stringToAdd2 += "+" + pair.Value + " Dexterity<br>";
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
}
