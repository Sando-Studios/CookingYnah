using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationarySprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool tiltSprite = true;

    private void Start()
    {   spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (tiltSprite)
        {
            Quaternion tilt = Quaternion.Euler(25, 0, 0);
            spriteRenderer.transform.rotation = tilt;
        }
        

        // Get the current X position of the object
        float zPosition = transform.position.z;

        // Calculate the sorting order based on the X value
        int sortingOrder = Mathf.RoundToInt(-zPosition * 100);

        // Update the sorting order of the sprite renderer
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
