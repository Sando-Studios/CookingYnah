using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigsMeleeAniEvent : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public void MeleeEnd()
    {
        enemy.SetIsAttackDone(true);
    }
}
