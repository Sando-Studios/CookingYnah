using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceZ : MonoBehaviour
{
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;
    }

    private void LateUpdate()
    {
        if (parentTransform != null)
        {
            // Get the current parent rotation
            Quaternion parentRotation = parentTransform.rotation;

            // Create a new rotation with only the Z-axis pointing forward
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, parentRotation.eulerAngles.z);

            // Apply the new rotation to the child transform
            transform.rotation = targetRotation;
        }
    }

}
