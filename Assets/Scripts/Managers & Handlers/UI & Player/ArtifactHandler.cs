using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public enum Artifacts
{
    None,
    Fire_Meatball,
    Ground_Wave,
    Axe_Slashes,
}
public class ArtifactHandler : MonoBehaviour
{
    private SerializedDictionary<int, Artifacts> artifacts = new SerializedDictionary<int, Artifacts>()
    {
        {1, Artifacts.None},
        {2, Artifacts.None}
    };

    [SerializeField] private GameObject playerGameObject;

    [SerializeField] private Image slot1Image;
    [SerializeField] private Image slot2Image;

    private void OnEnable()
    {
        ArtifactUIHandler.OnArtifactSelected += SetArtifact;
    }

    private void OnDisable()
    {
        ArtifactUIHandler.OnArtifactSelected -= SetArtifact;
    }

    public void SetArtifact(int slotNum, Artifacts artifactToSlot, GameObject obj, int staminaCost, Image image, Sprite sprite)
    {
        if (CanSlotArtifact(artifactToSlot))
        {
            // Remove ArtifactAbility script to be replaced
            ArtifactAbility artifactScript;
            switch (artifacts[slotNum])
            {
                case Artifacts.Fire_Meatball:
                    artifactScript = playerGameObject.GetComponent<FireMeatballAbility>();
                    artifactScript.enabled = false;
                    Destroy(artifactScript);
                    break;
                case Artifacts.Ground_Wave:
                    artifactScript = playerGameObject.GetComponent<GroundWaveAbility>();
                    artifactScript.enabled = false;
                    Destroy(artifactScript);
                    break;
                case Artifacts.Axe_Slashes:
                    artifactScript = playerGameObject.GetComponent<OmniSlashAbility>();
                    artifactScript.enabled = false;
                    Destroy(artifactScript);
                    break;
                case Artifacts.None:
                    break;
            }

            artifacts[slotNum] = artifactToSlot;

            Color currentColor = image.color;
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            image.color = newColor;
            image.sprite = sprite;

            if (slotNum.Equals(1))
            {
                slot1Image.sprite = sprite;
                slot1Image.color = Color.white;
            }

            else if (slotNum.Equals(2))
            {
                slot2Image.sprite = sprite;
                slot2Image.color = Color.white;
            }


            // Add ArtifactAbility script
            var a = artifactToSlot;
            switch (a)
            {
                case Artifacts.Fire_Meatball:
                    playerGameObject.AddComponent<FireMeatballAbility>();
                    playerGameObject.GetComponent<FireMeatballAbility>().SetAbilityData(obj, slotNum, staminaCost);
                    playerGameObject.GetComponent<FireMeatballAbility>().SetImage((slotNum.Equals(1)) ? slot1Image : slot2Image);
                    break;
                case Artifacts.Ground_Wave:
                    playerGameObject.AddComponent<GroundWaveAbility>();
                    playerGameObject.GetComponent<GroundWaveAbility>().SetAbilityData(obj, slotNum, staminaCost);
                    playerGameObject.GetComponent<GroundWaveAbility>().SetImage((slotNum.Equals(1)) ? slot1Image : slot2Image);

                    break;
                case Artifacts.Axe_Slashes:
                    playerGameObject.AddComponent<OmniSlashAbility>();
                    playerGameObject.GetComponent<OmniSlashAbility>().SetAbilityData(obj, slotNum, staminaCost);
                    playerGameObject.GetComponent<OmniSlashAbility>().SetImage((slotNum.Equals(1)) ? slot1Image : slot2Image);

                    break;
            }
        }
    }

    // Check if the artifact is already slotted
    private bool CanSlotArtifact(Artifacts artifactToSlot)
    {
        if (artifacts.ContainsValue(artifactToSlot))
            return false;

        return true;
    }
}
