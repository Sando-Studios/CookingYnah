using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TempBuffIcon : MonoBehaviour
{
    public Image image;
    private float duration;
    private float currentTime;
    private bool start = false;

    public void SetData(Sprite sprite, float duration)
    {
        image.sprite = sprite;
        this.duration = duration;
        currentTime = duration;

        start = true;
    }

    private void Update()
    {
        if (!start) return;

        float normalized = currentTime / duration;

        image.fillAmount = normalized;

        currentTime -= Time.deltaTime;
    }
}
