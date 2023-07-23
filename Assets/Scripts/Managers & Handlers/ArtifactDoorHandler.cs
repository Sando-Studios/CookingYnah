using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDoorHandler : MonoBehaviour
{
    [SerializeField] private ArtifactProgress artifactProgress;

    [Header("Artifact Icons")]
    [SerializeField] private SerializedDictionary<Artifacts, SpriteRenderer> ArtifactIconDictionary;

    private int unlockedArtifactCount = 0;
    [SerializeField] private GameObject doorObject;
    private bool isComplete = false;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        foreach (var artifact in artifactProgress.UnlockedArtifacts)
        {
            SetArtifactStatus(ArtifactIconDictionary[artifact.Key], artifact.Value);
        }

        if (unlockedArtifactCount >= 3)
        {
            isComplete = true;
            audioSource.Play();
        }
    }

    private void SetArtifactStatus(SpriteRenderer sprite, bool status)
    {
        if (status)
        {
            unlockedArtifactCount++;
            sprite.color = Color.white;
        }

        else
            sprite.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorObject)
        {
            if (isComplete && doorObject.transform.position.y >= -2.7f)
            {
                doorObject.transform.Translate(Vector3.down * Time.deltaTime);
            }
            else if (isComplete && doorObject.transform.position.y < -2.7f)
            {
                Destroy(doorObject);
            }
        }
    }
}
