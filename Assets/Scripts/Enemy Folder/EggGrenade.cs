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

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickingClip;
    [SerializeField] private AudioClip explosionClip;

    public static Action<int> OnEggnadeExplode;

    private Player playerUse;

    private void Start()
    {
        audioSource.clip = tickingClip;
        audioSource.loop = true;
        audioSource.Play();
        
    }
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

        audioSource.clip = explosionClip;
        audioSource.loop = false;
        audioSource.Play();

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
