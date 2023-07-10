using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;

public class ChickenEnemy : Enemy
{
    [Header("Egg Grenade")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform eggSpawnPoint;

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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //DealDamage()

            GameObject clone = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
            clone.GetComponent<ArcGrenade>().InitializeGrenade(Vector3.zero);
        }
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

    public override void DealDamage()
    {
        //base.DealDamage();
        if (targetUnit)
        {
            GameObject clone = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
            clone.GetComponent<ArcGrenade>().InitializeGrenade(targetUnit.transform.position);
            clone.GetComponent<EggGrenade>().SetExplosionData(enemyDataInstance.BasicAttackDamage);
        }
    }

}
