using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArtifactDescription", menuName = "Artifact/Artifact Description")]
public class ArtifactDescriptions : ScriptableObject
{
    [Header("Artifact Data")]
    [SerializeField] private string artifactName;
    [TextArea]
    [SerializeField] private string artifactDescription;

    public string Name { get { return artifactName; } }
    public string Description { get { return artifactDescription; } }


}
