using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CowEnemy : MinorEnemy
{
    [Header("Charge")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float maxChargeDistance;
    private Vector3 chargeEndPoint;
    private bool isCharging;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player") && !other.isTrigger && capsuleCollider.isTrigger)
        {
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), enemyDataInstance.BasicAttackDamage);
        }
    }

    public override void ExecuteAttack()
    {
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
                AttackTimer();
                SetIsAttackDone(true);
            }
            else
            {
                transform.position += movement * Time.deltaTime;
            }
        }

    }
}
