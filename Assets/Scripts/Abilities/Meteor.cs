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
    private List<Enemy> enemiesInRange = new();

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
        DamageTicker();
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
        Animator animator = GetComponentInChildren<Animator>();

        animator.SetBool("isExploding", true);

        move = false;
    }

    public void OnExplode()
    {
        gameObject.transform.position = gameObject.transform.position + Vector3.down * 10;
        Destroy(gameObject, 1.0f);
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
                enemiesInRange.Add(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.CompareTag("Enemy") && !isBoss)
                enemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }

    private async void DamageTicker()
    {
        while (true)
        {
            if (enemiesInRange.Count > 0)
            {
                foreach (Enemy e in enemiesInRange)
                {
                    DealDamage(e);
                }
            }

            await new WaitForSeconds(1.0f);
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
