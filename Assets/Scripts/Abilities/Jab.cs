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
    }

    private void Slow()
    {
        Debug.Log("pow");
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
