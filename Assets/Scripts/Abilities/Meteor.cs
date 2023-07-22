using Asyncoroutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool move;
    private int damageValue;
    private float strength;
    private bool isBoss;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }
    public void SetDamageValue(float dmg)
    {
        damageValue = (int)dmg;
        isBoss = true;
    }
    public void SetDamageValue(float dmg, float strength)
    {
        SetDamageValue((int)dmg);
        this.strength = strength;
        isBoss = false;
    }

    public void TriggerMove()
    {
        move = true;
    }

    private void Update()
    {
        float speed = 5f;

        if (move)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget <= 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Animations


        move = false;
        gameObject.transform.position = gameObject.transform.position + Vector3.down * 10;
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.CompareTag("Player") && isBoss)
            {
                DealDamage(other.GetComponent<Player>());
                Explode();
            }
            else if (other.CompareTag("Enemy") && !isBoss)
                DealDamage(other.GetComponent<Enemy>());
        }
    }

    private void DealDamage(Player player)
    {
        DamageHandler.ApplyDamage(player, damageValue);
    }

    private void DealDamage(Enemy enemy)
    {
        DamageHandler.ApplyDamage(enemy, damageValue, strength);
    }
}
