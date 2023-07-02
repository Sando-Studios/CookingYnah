using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
public enum Artifacts
{
    None,
    Fire_Meatball,
}
public class ArtifactHandler : MonoBehaviour
{
    private SerializedDictionary<int, Artifacts> artifacts = new SerializedDictionary<int, Artifacts>()
    {
        {1, Artifacts.None},
        {2, Artifacts.None}
    };

    [SerializeField] private GameObject playerGameObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetArtifact(1, Artifacts.Fire_Meatball);
        }
    }

    public void SetArtifact(int slotNum, Artifacts artifactToSlot)
    {
        if (CanSlotArtifact(artifactToSlot))
        {
            // Remove Artifact script to be replaced
            switch (artifacts[slotNum])
            {
                case Artifacts.Fire_Meatball:
                    Artifact artifactScript = playerGameObject.GetComponent<FireMeatballArtifact>();
                    artifactScript.enabled = false;
                    Destroy(artifactScript);
                    break;
                case Artifacts.None:
                    break;
            }

            artifacts[slotNum] = artifactToSlot;

            // Add Artifact script
            var a = artifactToSlot;
            switch (a)
            {
                case Artifacts.Fire_Meatball:
                    playerGameObject.AddComponent<FireMeatballArtifact>();
                    break;
            }
        }
    }

    private bool CanSlotArtifact(Artifacts artifactToSlot)
    {
        // Check if the artifact is already slotted
        if (artifacts.ContainsValue(artifactToSlot))
            return false;

        return true;
    }
}
