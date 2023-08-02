using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackFinished()
    {
        animator.SetTrigger("Attack Finish");
        animator.ResetTrigger("Attack Start");
        animator.ResetTrigger("TempestTrigger");
    }
}
