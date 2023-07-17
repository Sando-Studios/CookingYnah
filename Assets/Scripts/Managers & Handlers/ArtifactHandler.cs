using AYellowpaper.SerializedCollections;
using UnityEngine;
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

    private void OnEnable()
    {
        ArtifactUIHandler.OnArtifactSelected += SetArtifact;
    }

    private void OnDisable()
    {
        ArtifactUIHandler.OnArtifactSelected -= SetArtifact;
    }

    public void SetArtifact(int slotNum, Artifacts artifactToSlot, GameObject obj)
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

            // Add ArtifactAbility script
            var a = artifactToSlot;
            switch (a)
            {
                case Artifacts.Fire_Meatball:
                    playerGameObject.AddComponent<FireMeatballAbility>();
                    playerGameObject.GetComponent<FireMeatballAbility>().SetAbilityData(obj, slotNum);
                    break;
                case Artifacts.Ground_Wave:
                    playerGameObject.AddComponent<GroundWaveAbility>();
                    playerGameObject.GetComponent<GroundWaveAbility>().SetAbilityData(obj, slotNum);
                    break;
                case Artifacts.Axe_Slashes:
                    playerGameObject.AddComponent<OmniSlashAbility>();
                    playerGameObject.GetComponent<OmniSlashAbility>().SetAbilityData(obj, slotNum);
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
