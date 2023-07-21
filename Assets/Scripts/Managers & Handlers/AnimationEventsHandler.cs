using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] protected Enemy enemy;
    public virtual void AttackAnimationEnds()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        enemy.SetIsAttackDone(true);
    }
}
