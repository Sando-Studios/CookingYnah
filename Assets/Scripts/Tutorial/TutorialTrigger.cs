using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInventory swap;

    private void Start()
    {
        // swap = UIManager.instance.player.Swap(swap);
        swap = UIManager.instance.SwapInventory(swap);
    }

    private void OnDestroy()
    {
        swap = UIManager.instance.SwapInventory(swap);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out var player)) return;

        UIManager.instance.craftingBtnBlock = true;
        UIManager.instance.ForceOpenCraftingPanel();

        UIManager.instance.CraftingStartTutorialSequence();
        UIManager.instance.player.DisableInputs();
    }
}
