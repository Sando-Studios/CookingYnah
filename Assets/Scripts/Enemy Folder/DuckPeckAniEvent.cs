using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPeckAniEvent : MonoBehaviour
{
    [SerializeField] private DuckEnemy duckEnemy;

    public void PeckEnd()
    {
        duckEnemy.SetIsAttackDone(true);
    }
}
