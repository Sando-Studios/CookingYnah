using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactAbility : MonoBehaviour
{

    [Header("Spawning")]
    [SerializeField] protected GameObject attackPrefab;

    [SerializeField] private int slotNum;
    [SerializeField] private int staminaCost = 30;
    protected bool isAbilityActive = false;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (slotNum > 0 && Input.GetButtonDown("Artifact " + slotNum.ToString()) && gameObject.GetComponent<Player>())
        {
            PlayerUnitData playerData = GetComponent<Player>().GetPlayerData();
            if (staminaCost <= playerData.CurrentStamina && !isAbilityActive)
            {
                UseSpecialAttack();
                playerData.CurrentStamina -= staminaCost;
            }   
            else { /*Play error sound*/ }

        }
    }

    protected abstract void UseSpecialAttack();

    public virtual void SetAbilityData(GameObject obj, int slotIndex, int cost)
    {
        attackPrefab = obj;
        slotNum = slotIndex;
        staminaCost = cost;
    }
}
