using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image image;

    public void SetData(string name, Sprite sprite)
    {
        nameText.text = name;
        image.sprite = sprite;
        Destroy(gameObject, 3.0f);
    }
}
