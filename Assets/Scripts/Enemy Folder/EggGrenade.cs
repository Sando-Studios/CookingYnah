using Asyncoroutine;
using System;
using UnityEngine;

public class EggGrenade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isOnFloor = false;

    [Header("Explosion Data")]
    [SerializeField] private float explosionRadius = 4.0f;
    [SerializeField] private int explosionDamage = 4;
    private bool isExploding = false;
    private int ownerID;

    public static Action<int> OnEggnadeExplode;

    private Player playerUse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") && !isOnFloor && !other.isTrigger)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            isOnFloor = true;

            ExplosionTimer();
        }

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerUse = other.GetComponent<Player>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerUse = null;
        }
    }

    public void SetExplosionData(int dmg, int owner)
    {
        ownerID = owner;
        explosionDamage = dmg;
    }

    private async void ExplosionTimer()
    {
        GetComponent<SphereCollider>().radius = explosionRadius;
        await new WaitForSeconds(4.0f);
        DamageHandler.ApplyDamage(playerUse, explosionDamage);
        isExploding = true;
        animator.SetBool("isExploding", isExploding);
           
    }

    private void DestroyObject()
    {
        OnEggnadeExplode?.Invoke(ownerID);
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject);
    }
}
