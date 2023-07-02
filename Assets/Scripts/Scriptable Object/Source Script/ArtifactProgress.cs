using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactProgress", menuName = "Artifact/ArtifactProgress")]
public class ArtifactProgress : ScriptableObject
{
    [SerializeField] private SerializedDictionary<Artifacts, bool> isArtifactUnlocked = new SerializedDictionary<Artifacts, bool>();

    public SerializedDictionary<Artifacts, bool> UnlockedArtifacts
    {
        get { return isArtifactUnlocked; }
        set { isArtifactUnlocked = value; }
    }
}
