using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowEnemy : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void SetEnemyData(int enemyID, EnemyUnitData unitData, Vector3 homeBase)
    {
        base.SetEnemyData(enemyID, unitData, homeBase);
    }

    public override bool IsAlive()
    {
        return base.IsAlive();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ResetAggro()
    {
        base.ResetAggro();
    }

    public override Vector3 GetHome()
    {
        return base.GetHome();
    }
    public override GameObject GetTargetUnit()
    {
        return base.GetTargetUnit();
    }
    public override EnemyUnitData GetEnemyUnitData()
    {
        return base.GetEnemyUnitData();
    }

    protected override void Death(int id)
    {
        base.Death(id);
    }

    protected override ItemData GetRandomItemData()
    {
        return base.GetRandomItemData();
    }

    public override void DealDamage()
    {
        base.DealDamage();
    }
}
