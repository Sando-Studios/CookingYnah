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
    public void TriggerHitCheck()
    {
        MajorEnemy majorEnemy = transform.parent.GetComponent<MajorEnemy>();

        majorEnemy.CheckBasicAttackHit();
    }
    public void TriggerMeleeHitCheck()
    {
        MinorEnemy minorEnemy = transform.parent.GetComponent<MinorEnemy>();

        minorEnemy.CheckBasicAttackHit();
    }
    public void TriggerWave()
    {
        GoatMajorEnemy goatEnemy = transform.parent.GetComponent<GoatMajorEnemy>();
        goatEnemy.StartWave();
        goatEnemy.SetIsAttackDone(true);
    }
    public void TriggerBall()
    {
        TurkeyMajorEnemy turkeyEnemy = transform.parent.GetComponent<TurkeyMajorEnemy>();
        turkeyEnemy.SpawnAttack();
    }

    public void AddAttackCount()
    {
        MajorEnemy majorEnemy = transform.parent.GetComponent<MajorEnemy>();

        majorEnemy.AddToAttackCount(1);
    }
    public void OnExplosionEnd()
    {
        transform.root.GetComponent<Meteor>().OnExplode();
    }
}
