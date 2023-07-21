using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    protected Enemy enemy;
    public virtual void AttackAnimationEnds()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        enemy.SetIsAttackDone(true);
    }

    public void TriggerWave()
    {
        GoatMajorEnemy goatEnemy = transform.parent.GetComponent<GoatMajorEnemy>();
        goatEnemy.StartWave();
        goatEnemy.SetIsAttackDone(true);
    }
}
