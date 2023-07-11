using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowEnemy : Enemy
{
    [Header("Charge")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float maxChargeDistance;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player") && !other.isTrigger && capsuleCollider.isTrigger)
        {
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), enemyDataInstance.BasicAttackDamage);
        }
    }

    public override void DoAttack()
    {

        StartCharge();
    }

    private void StartCharge()
    {
        Vector3 direction = targetUnit.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        Vector3 destination = transform.position + direction * maxChargeDistance;

        NavMesh.SamplePosition(destination, out NavMeshHit point, 5.0f, NavMesh.AllAreas);

        agent.SetDestination(point.position);

        capsuleCollider.isTrigger = true;
    }

    protected override void FixedUpdate()
    {
        if (capsuleCollider.isTrigger && transform.position == agent.destination)
        {
            capsuleCollider.isTrigger = false;
        }
    }
}
