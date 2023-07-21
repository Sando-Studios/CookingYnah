using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCraftingStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cone")) return;

        var player = other.gameObject.GetComponentInParent<Player>();

        if (!player) return;
        
        player.isAtCookingStation = true;
        UIManager.instance.SetCraftingPopUp();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cone")) return;

        var player = other.gameObject.GetComponentInParent<Player>();

        if (!player) return;
        
        player.isAtCookingStation = false;
        UIManager.instance.SetCraftingPopUp();
    }
}
