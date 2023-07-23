using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out var player)) return;

        UIManager.instance.craftingBtnBlock = true;
        UIManager.instance.ForceOpenCraftingPanel();

        UIManager.instance.CraftingStartTutorialSequence();
        UIManager.instance.player.DisableInputs();
    }
}
