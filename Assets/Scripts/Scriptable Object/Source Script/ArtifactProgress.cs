using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactProgress", menuName = "Artifact/Artifact Progress")]
public class ArtifactProgress : ScriptableObject
{
    [SerializeField] private SerializedDictionary<Artifacts, bool> isArtifactUnlocked = new SerializedDictionary<Artifacts, bool>();

    [SerializeField] private SerializedDictionary<Artifacts, ArtifactDescriptions> artifactDescriptionDictionary = new SerializedDictionary<Artifacts, ArtifactDescriptions>();

    public SerializedDictionary<Artifacts, bool> UnlockedArtifacts
    {
        get { return isArtifactUnlocked; }
        set { isArtifactUnlocked = value; }
    }

    public SerializedDictionary<Artifacts, ArtifactDescriptions> ArtifactDescriptions
    {
        get { return artifactDescriptionDictionary; }
    }
}
