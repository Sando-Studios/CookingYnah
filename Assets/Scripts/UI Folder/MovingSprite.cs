using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingSprite : MonoBehaviour
{
    private SortingGroup spriteGroup;

    private void Awake()
    {
        spriteGroup = GetComponent<SortingGroup>();
    }

    private void Update()
    {
        // Get the current X position of the object
        float zPosition = transform.position.z;

        // Calculate the sorting order based on the X value
        int sortingOrder = Mathf.RoundToInt(-zPosition * 100);

        // Update the sorting order of the sprite renderer
        spriteGroup.sortingOrder = sortingOrder;
    }
}
