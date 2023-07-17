using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] protected MinorEnemy enemy;
    public virtual void AttackAnimationEnds()
    {
        enemy.SetIsAttackDone(true);
    }
}
