using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPosition : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);

            rectTransform.position = targetPosition;
        }
    }
}
