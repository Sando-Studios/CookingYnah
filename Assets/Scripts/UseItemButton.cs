using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;

public class UseItemButton : MonoBehaviour
{
    //public SerializedDictionary<PlayerStat, int> buff = new SerializedDictionary<PlayerStat, int>();

    public Button button;
    public string itemToUse;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = UIManager.instance.player.GetInventory();
    }

    private void Update()
    {
        button.interactable = CheckInventory();
    }
    private bool CheckInventory()
    {
        if (playerInventory.GetItemQuantity(itemToUse) > 0)
        {
            return true;
        }
        return false;
    }

}
