using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using UnityEngine.SceneManagement;

public class HealthRegen : MonoBehaviour
{
    private PlayerUnitData playerUnitData;
    private bool inHomeBase;
    [SerializeField] private float healInterval = 10.0f;
    [SerializeField] private int healAmount = 1;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        playerUnitData = GetComponent<Player>().GetPlayerData();
        RegenHP();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Main"))
            inHomeBase = true;
        else
            inHomeBase = false;
    }

    private async void RegenHP()
    {
        while (true)
        {
            await new WaitForSeconds(healInterval);
            if (inHomeBase)
            {
                playerUnitData.CurrentHealth += healAmount;

                if (playerUnitData.CurrentHealth > playerUnitData.MaxHealth)
                    playerUnitData.CurrentHealth = playerUnitData.MaxHealth;

                UIManager.instance.UpdateHpBarUI();
            }
        }
    }
}
