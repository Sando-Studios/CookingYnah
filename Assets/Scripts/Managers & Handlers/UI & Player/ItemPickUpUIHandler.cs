using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;

    private void OnEnable()
    {
        Item.OnItemPickUp += AddItemPopup;
    }
    private void OnDisable()
    {
        Item.OnItemPickUp -= AddItemPopup;
    }

    private void AddItemPopup(string name, Sprite sprite)
    {
        GameObject clone = Instantiate(popUpPrefab, transform);
        clone.GetComponent<PickUpPopUp>().SetData(name, sprite);
    }
}
