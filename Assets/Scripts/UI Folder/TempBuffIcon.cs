// Ignore Spelling: vit

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TempBuffIcon : MonoBehaviour
{
    public Image image;
    public Sprite vitSprite;
    public Sprite strSprite;
    public Sprite resSprite;

    
    private float duration;
    private float currentTime;
    private bool start = false;

    private void OnEnable()
    {
        DamageHandler.OnPlayerUnitDeath += PlayerKilled;
    }
    private void OnDisable()
    {
        DamageHandler.OnPlayerUnitDeath -= PlayerKilled;
    }

    private void PlayerKilled(int i)
    {
        Destroy(gameObject);
    }

    public void SetData(TargetStat stat, float duration)
    {
        var s = stat;
        switch(s)
        {
            case TargetStat.VitStat:
                image.sprite = vitSprite;
                break;
            case TargetStat.StrStat:
                image.sprite = strSprite;
                break;
            case TargetStat.ResStat:
                image.sprite = resSprite;
                break;
        }

        this.duration = duration;
        currentTime = duration;

        start = true;
        
        Destroy(gameObject, this.duration);
    }

    private void Update()
    {
        if (!start) return;

        float normalized = currentTime / duration;

        image.fillAmount = normalized;

        currentTime -= Time.deltaTime;
    }
}
