using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Stats Texts")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI vitText;
    [SerializeField] private TextMeshProUGUI agiText;
    [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private TextMeshProUGUI vigText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI dexText;

    private void Start()
    {
        playerData = player.GetPlayerData();
        playerInventory = player.GetInventory();
        UpdateStatsUI();
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
        playerInventory.RemoveItem("Drop Item(Clone)");
    }
    
}
