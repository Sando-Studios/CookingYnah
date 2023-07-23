using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCameraHorizon : MonoBehaviour
{
    private Camera came;

    [Min(1)]
    [SerializeField] private float speed = 10f;
    
    private void Start()
    {
        came = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var target = Quaternion.LookRotation(GetDirection());
        transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
    }
    
    private Vector3 GetDirection()
    {
        // Getting direction towards mouse
        Vector3 startingPos = transform.position;
        Ray ray = came.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, startingPos);

        float distance;
        Vector3 endingPos = Vector3.zero;

        if (plane.Raycast(ray, out distance))
        {
            endingPos = ray.GetPoint(distance);
        }

        Vector3 direction = endingPos - startingPos;
        direction.y = 0f;

        return direction.normalized;
    }
}
