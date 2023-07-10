using UnityEngine;


public class ChickenEnemy : Enemy
{
    [Header("Egg Grenade")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform eggSpawnPoint;

    public override void SetEnemyData(int enemyID, EnemyUnitData unitData, Vector3 homeBase)
    {
        base.SetEnemyData(enemyID, unitData, homeBase);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
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
