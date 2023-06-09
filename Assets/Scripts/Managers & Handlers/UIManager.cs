using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.ExceptionServices;

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

    [Header("Stats UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI vitText;
    [SerializeField] private TextMeshProUGUI agiText;
    [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private TextMeshProUGUI vigText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI dexText;

    [Header("Inventory UI")]
    [SerializeField] private GameObject itemNodePrefab;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSlotParent;


    private void Start()
    {
        playerData = player.GetPlayerData();
        playerInventory = player.GetInventory();
        UpdateStatsUI();
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
    }

}
