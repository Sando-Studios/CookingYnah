using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class StaminaRegen : MonoBehaviour
{
    private PlayerUnitData playerUnitData;
    [SerializeField] private float regenInterval = 0.3f;
    [SerializeField] private int regenAmount = 1;

    private void Start()
    {
        playerUnitData = GetComponent<Player>().GetPlayerData();
        RegenStamina();
    }

    private async void RegenStamina()
    {
        while (true)
        {
            await new WaitForSeconds(regenInterval);
            if (playerUnitData.CurrentStamina < playerUnitData.MaxStamina)
            {
                playerUnitData.CurrentStamina += regenAmount;

                if (playerUnitData.CurrentStamina > playerUnitData.MaxStamina)
                    playerUnitData.CurrentStamina = playerUnitData.MaxStamina;
            }
        }
    }
}
