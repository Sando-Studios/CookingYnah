using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class Bocchi : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private int damageValue;
    private float strength;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
        Grow(gameObject);
    }

    public async void Grow(GameObject obj)
    {
        while (obj)
        {
            Vector3 newScale = gameObject.transform.localScale;
            newScale.y += 0.5f;
            gameObject.transform.localScale = newScale;
            await new WaitForSeconds(0.2f);
        }
    }

    public void SetDamageValue(float dmg)
    {
        damageValue = (int)dmg;
    }
    public void SetDamageValue(float dmg, float strength)
    {
        SetDamageValue((int)dmg);
        this.strength = strength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.CompareTag("Player"))
                DealDamage(other.GetComponent<Player>());

            if (other.gameObject.CompareTag("MinorEnemy"))
                DealDamage(other.GetComponent<MinorEnemy>());
        }
    }

    private void DealDamage(Player player)
    {
        DamageHandler.ApplyDamage(player, damageValue);
    }
    private void DealDamage(MinorEnemy enemy)
    {
        DamageHandler.ApplyDamage(enemy, damageValue, strength);
    }
}
