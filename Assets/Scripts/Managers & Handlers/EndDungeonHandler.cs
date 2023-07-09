using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDungeonHandler : MonoBehaviour
{
    private int bossID;
    private GameObject bossExit;

    [SerializeField] private Color disabledColor;
    [SerializeField] private Color enabledColor;

    private void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += OnBossDeath;
    }

    private void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= OnBossDeath;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossExit = gameObject;

        bossExit.GetComponent<SpriteRenderer>().color = disabledColor;
        bossExit.GetComponent<SceneChangeHandler>().SetCanUse(false);
    }

    public void SetBossID(int id)
    {
        bossID = id;
    }

    private void OnBossDeath(int id)
    {
        if (id == bossID)
        {
            bossExit.GetComponent<SpriteRenderer>().color = enabledColor;
            bossExit.GetComponent<SceneChangeHandler>().SetCanUse(true);

        }
    }
}
