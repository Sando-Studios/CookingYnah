using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ArtifactAbility : MonoBehaviour
{

    [Header("Spawning")]
    [SerializeField] protected GameObject attackPrefab;

    [SerializeField] private int slotNum;
    [SerializeField] private int staminaCost = 30;
    protected bool isAbilityActive = false;
    protected Image uiArtifactImage;

    public static Action<int> OnAbilityFail;

    private bool isBoss = true;

    protected virtual void Start()
    {
        if (!gameObject.CompareTag("Enemy")) isBoss = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isBoss) return;

        if (slotNum > 0 && Input.GetButtonDown("Artifact " + slotNum.ToString()) && gameObject.GetComponent<Player>())
        {
            PlayerUnitData playerData = GetComponent<Player>().GetPlayerData();
            if (staminaCost <= playerData.CurrentStamina && !isAbilityActive)
            {
                UseSpecialAttack();
                playerData.CurrentStamina -= staminaCost;
                UIManager.instance.UpdateStaminaBarUI();
            }
            else { OnAbilityFail?.Invoke(0); }

        }

        float alpha = (staminaCost <= GetComponent<Player>().GetPlayerData().CurrentStamina) ?  1.0f: 0.5f;
        uiArtifactImage.color = new Color(uiArtifactImage.color.r, uiArtifactImage.color.g, uiArtifactImage.color.b, alpha);
    }
    protected abstract void UseSpecialAttack();

    public virtual void SetAbilityData(GameObject obj, int slotIndex, int cost)
    {
        attackPrefab = obj;
        slotNum = slotIndex;
        staminaCost = cost;
    }
    public virtual void SetImage(Image image)
    {
        uiArtifactImage = image;
    }
}
