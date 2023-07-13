using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenShootAniEvent : MonoBehaviour
{
    [SerializeField] private ChickenEnemy chickenEnemy;

    public void LauchEgg()
    {
        chickenEnemy.EnableEggnade();
    }
    public void ShootAnimationEnd()
    {
        chickenEnemy.SetIsAttackDone(true);
    }
}
