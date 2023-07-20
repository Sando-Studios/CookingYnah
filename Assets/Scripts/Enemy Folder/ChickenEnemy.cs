using UnityEngine;
using UnityEngine.AI;

public class ChickenEnemy : MinorEnemy
{
    [Header("Egg Grenade")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform eggSpawnPoint;
    private bool hasGrenadeOut;
    private GameObject spawnedEgg;

    protected override void OnEnable()
    {
        base.OnEnable();
        DamageHandler.OnEnemyUnitDeath += base.Death;
        EggGrenade.OnEggnadeExplode += EggGrenadeExplode;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        DamageHandler.OnEnemyUnitDeath -= base.Death;
        EggGrenade.OnEggnadeExplode -= EggGrenadeExplode;
    }

    private void EggGrenadeExplode(int id)
    {
        if (enemyDataInstance.UnitID == id)
        {
            hasGrenadeOut = false;
            AttackTimer(enemyDataInstance.AttackSpeed);
        }
    }

    public void EnableEggnade()
    {
        spawnedEgg.GetComponent<ArcGrenade>().InitializeGrenade(targetUnit.transform.position);
        spawnedEgg.transform.position = eggSpawnPoint.position;
        spawnedEgg.SetActive(true);
    }

    public override void ExecuteAttack()
    {
        if (targetUnit && !hasGrenadeOut)
        {
            hasGrenadeOut = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            GameObject clone = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
            clone.SetActive(false);
            clone.GetComponent<EggGrenade>().SetExplosionData(enemyDataInstance.BasicAttackDamage, enemyDataInstance.UnitID);
            spawnedEgg = clone;
        }
    }

    protected override void Death(int id)
    {
        base.Death(id);
    }
}
