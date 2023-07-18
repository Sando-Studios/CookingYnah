using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDungeonHandler : MonoBehaviour
{
    private GameObject bossExit;

    [SerializeField] private Color disabledColor;

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
        bossExit = gameObject;

        bossExit.GetComponent<SpriteRenderer>().color = disabledColor;
        bossExit.GetComponent<SceneChangeHandler>().SetCanUse(false);
    }


    private void OnBossDeath(Artifacts artifact, string name)
    {
            bossExit.GetComponent<SpriteRenderer>().color = Color.white;
            bossExit.GetComponent<SceneChangeHandler>().SetCanUse(true);
    }
}
