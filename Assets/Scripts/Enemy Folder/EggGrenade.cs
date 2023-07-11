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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") && !isOnFloor && !other.isTrigger)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            isOnFloor = true;

            ExplosionTimer();
        }

        if (other.CompareTag("Player") && isOnFloor && isExploding && !other.isTrigger)
        {
            DamageHandler.ApplyDamage(other.GetComponent<Player>(), explosionDamage);

        }
    }

    public void SetExplosionData(int dmg, int owner)
    {
        ownerID = owner;
        explosionDamage = dmg;
    }

    private async void ExplosionTimer()
    {
        await new WaitForSeconds(4.0f);
        isExploding = true;
        GetComponent<SphereCollider>().radius = explosionRadius;
        animator.SetBool("isExploding", isExploding);
           
    }

    private void DestroyObject()
    {
        OnEggnadeExplode?.Invoke(ownerID);
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject);
    }
}
