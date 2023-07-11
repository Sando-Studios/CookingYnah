using UnityEngine;


public class ChickenEnemy : Enemy
{
    [Header("Egg Grenade")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform eggSpawnPoint;
    private bool hasGrenadeOut;
    private GameObject spawnedEgg;

    protected override void OnEnable()
    {
        base.OnEnable();
        EggGrenade.OnEggnadeExplode += EggGrenadeExplode;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EggGrenade.OnEggnadeExplode -= EggGrenadeExplode;
    }

    private void EggGrenadeExplode(int id)
    {
        if (enemyDataInstance.UnitID == id)
        {
            hasGrenadeOut = false;
            AttackTimer();
        }
    }

    public void EnableEggnade()
    {
        spawnedEgg.SetActive(true);
    }

    public override void DoAttack()
    {
        if (targetUnit && !hasGrenadeOut)
        {
            hasGrenadeOut = true;

            GameObject clone = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
            clone.SetActive(false);
            clone.GetComponent<ArcGrenade>().InitializeGrenade(targetUnit.transform.position);
            clone.GetComponent<EggGrenade>().SetExplosionData(enemyDataInstance.BasicAttackDamage, enemyDataInstance.UnitID);
            spawnedEgg = clone;
        }
    }
}
