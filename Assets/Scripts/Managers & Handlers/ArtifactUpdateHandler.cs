using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactUpdateHandler : MonoBehaviour
{
    [SerializeField] private ArtifactProgress progress;

    private void OnEnable()
    {
        DamageHandler.OnBossUnitDeath += UpdateArtifactProgress;
    }
    private void OnDisable()
    {
        DamageHandler.OnBossUnitDeath -= UpdateArtifactProgress;
    }

    private void UpdateArtifactProgress(Artifacts artifact, string name)
    {
        var a = artifact;
        switch (a)
        {
            case Artifacts.Fire_Meatball:
                progress.UnlockedArtifacts[a] = true;
                break;
            case Artifacts.Ground_Wave:
                progress.UnlockedArtifacts[a] = true;
                break;
            case Artifacts.Axe_Slashes:
                progress.UnlockedArtifacts[a] = true;
                break;
        }
    }
}
