using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDungeonHandler : MonoBehaviour
{
    private GameObject bossExit;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip triumph;

    private void OnEnable()
    {
        DamageHandler.OnBossUnitDeath += OnBossDeath;
    }

    private void OnDisable()
    {
        DamageHandler.OnBossUnitDeath -= OnBossDeath;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Stop();
        audioSource.clip = triumph;
        audioSource.Play();
        bossExit = gameObject;
        bossExit.GetComponent<SceneChangeHandler>().SetCanUse(false);
    }


    private void OnBossDeath(Artifacts artifact, string name)
    {
            bossExit.GetComponent<SceneChangeHandler>().SetCanUse(true);
    }
}
