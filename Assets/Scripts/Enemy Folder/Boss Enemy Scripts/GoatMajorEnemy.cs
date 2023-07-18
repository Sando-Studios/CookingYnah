using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMajorEnemy : MajorEnemy
{
    [Header("Charge")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float maxChargeDistance = 5.0f;
    private Vector3 chargeEndPoint;
    private bool isCharging;

    // Start is called before the first frame update
    protected void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);

        if (other.CompareTag("Player") && !other.isTrigger && capsuleCollider.isTrigger)
        {
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), enemyDataInstance.BasicAttackDamage);
        }
    }

    public override void ExecuteBasicAttack()
    {
        Debug.Log("Basic");
        if (!isCharging)
        {
            isCharging = true;

            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            Vector3 destination = transform.position + direction * maxChargeDistance;

            NavMesh.SamplePosition(destination, out NavMeshHit point, 3.0f, NavMesh.AllAreas);

            chargeEndPoint = point.position;
            agent.enabled = false;
            capsuleCollider.isTrigger = true;
        }
    }

    public override void ExecuteSpecialAttack()
    {
        AttackTimer(bossDataInstance.SpecialAttackSpeed);
        GroundWaveAbility groundWave = GetComponent<GroundWaveAbility>();
        groundWave.SpawnRocks(3, bossDataInstance.SpecialAttackDamage);
        SetIsAttackDone(true);
    }

    protected override void Update()
    {
        base.Update();
        if (isCharging)
        {
            Vector3 direction = chargeEndPoint - transform.position;
            direction.Normalize();
            Vector3 movement = direction * enemyDataInstance.ChaseSpeed;

            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            float distanceToTarget = Vector3.Distance(transform.position, chargeEndPoint);

            if (distanceToTarget <= 0.1f)
            {
                isCharging = false;
                capsuleCollider.isTrigger = false;
                agent.enabled = true;
                AttackTimer(enemyDataInstance.AttackSpeed);
                SetIsAttackDone(true);
            }
            else
            {
                transform.position += movement * Time.deltaTime;
            }
        }

    }
}
