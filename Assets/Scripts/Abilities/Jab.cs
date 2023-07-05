using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class Jab : MonoBehaviour
{
    [SerializeField] [Min(0)]
    private float timeBetweenAttacks;

    private Coroutine timer = null;

    [SerializeField] private uint threshold = 3;

    [Range(0, 500)]
    [SerializeField] private float range;

    [SerializeField] [Min(0)] private float damage;
    [Range(1, 100)] [SerializeField] private float damageMultiplier;

    internal uint internalCounter = 0;

    public void Attack()
    {
        StartTimer();

        if (internalCounter >= threshold)
        {
            Slow();
            internalCounter = 0;
            return;
        }
        
        Fast();
    }

    private void Fast()
    {
        Debug.Log("boom");
        
        if (!RayCast(out var obj)) return;
        
        DamageHandler.ApplyDamage(obj.GetComponent<Enemy>(), Convert.ToInt32(damage));
    }

    private void Slow()
    {
        Debug.Log("pow");
        
        if (!RayCast(out var obj)) return;

        var dmg = damage * damageMultiplier;

        DamageHandler.ApplyDamage(obj.GetComponent<Enemy>(), Convert.ToInt32(dmg));
    }

    // TODO: Maybe have this function be common on all abilities. i.e. Put on base class; Have a utility class; etc 
    private Vector3 GetDirection()
    {
        // Getting direction towards mouse
        Vector3 startingPos = transform.position; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

    private bool RayCast(out GameObject obj)
    {
        obj = null;
        
        Debug.DrawLine(transform.position, transform.position + GetDirection() * range, Color.blue, 1f, false);
        
        var ray = new Ray(transform.position, GetDirection());
        if (!Physics.Raycast(ray, out var info, range)) return false;
        
        Debug.DrawLine(transform.position, info.point, Color.red, 1f, false);
        
        Debug.Log($"hit {info.collider.name}");

        obj = info.collider.gameObject;

        return true;
    }

    public void Update()
    {
        // TODO: Remove testing code
        if (!Input.GetMouseButtonDown(1)) return;
        
        Attack();
    }

    private IEnumerator _timerReset()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        internalCounter = 0;
        timer = null;
    }

    private void StartTimer()
    {
        internalCounter++;
        if (timer == null)
            timer = StartCoroutine(_timerReset());
    }

}
