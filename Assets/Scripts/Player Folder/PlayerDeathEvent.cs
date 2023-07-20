using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathEvent : MonoBehaviour
{
    private void OnEnable()
    {
        DamageHandler.OnPlayerUnitDeath += OnPlayerDeathEvent;
    }

    private void OnDisable()
    {
        DamageHandler.OnPlayerUnitDeath -= OnPlayerDeathEvent;
    }

    private void OnPlayerDeathEvent(int id)
    {
        BuffManager.instance.RemoveAllTempBuffs();
        SceneManager.LoadScene("Main Level");
    }
}
