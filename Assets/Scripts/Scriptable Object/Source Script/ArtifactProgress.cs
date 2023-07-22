using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactProgress", menuName = "Artifact/Artifact Progress")]
public class ArtifactProgress : ScriptableObject
{
    [SerializeField] private SerializedDictionary<Artifacts, bool> isArtifactUnlocked = new SerializedDictionary<Artifacts, bool>();

    [SerializeField] private SerializedDictionary<Artifacts, ArtifactData> artifactDescriptionDictionary = new SerializedDictionary<Artifacts, ArtifactData>();

    public SerializedDictionary<Artifacts, bool> UnlockedArtifacts
    {
        get { return isArtifactUnlocked; }
        set { isArtifactUnlocked = value; }
    }

    public SerializedDictionary<Artifacts, ArtifactData> ArtifactDescriptions
    {
        get { return artifactDescriptionDictionary; }
    }

    public void UnlockAll()
    {
        var keys = new List<Artifacts>();
        
        foreach (var (key, _) in isArtifactUnlocked)
        {
            keys.Add(key);
        }
        
        foreach (var artifacts in keys)
        {
            isArtifactUnlocked[artifacts] = true;
        }
    }
}
