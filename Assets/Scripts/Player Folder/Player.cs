using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [Header("Unit DropData")]
    [SerializeField] private PlayerUnitData playerUnitData;
    private Rigidbody rb;
    public float force;

    [Header("OmniAttack")]
    [SerializeField] private OmniAttack attack;
    [SerializeField] private SphereCollider attackCollider;
    private bool canAttack = true;

    [SerializeField] private Jab jab;

    [Header("Inventory")]
    [SerializeField] private PlayerInventory inventory;

    public bool isAtCookingStation = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem attackParticle;
    [SerializeField] private GameObject spriteObject;

    private bool movementEnabled = true;

    [Header("Audio")]
    [SerializeField] private YnahWalkingSounds audioScript;

    [SerializedDictionary("Sound", "Sound File")]
    [SerializeField] private SerializedDictionary<int, AudioClip> ynahAttackSound;

    [SerializeField] private AudioSource ynahAttackAudio;
    public AudioSource ynahHurtSound;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        inventory = GetComponent<PlayerInventory>();

        playerUnitData.CurrentHealth = playerUnitData.MaxHealth;
        playerUnitData.CurrentStamina = playerUnitData.MaxStamina;

        UIManager.instance.UpdateHpBarUI();
        BuffManager.instance.SetPlayer(playerUnitData);

        if (SceneChangeManager.instance.GetObjectToLoad() != gameObject) { Destroy(gameObject); }
    }

    private void AddForce(Vector3 dir, out bool isMoving)
    {
        rb.AddForce(dir * force * Time.deltaTime, ForceMode.Force);
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (!movementEnabled) return;

        bool isMoving = false;

        if (Input.GetButton("Horizontal"))
        {
            var val = Input.GetAxis("Horizontal");
            AddForce(Vector3.right * val, out isMoving);
        }

        if (Input.GetButton("Vertical"))
        {
            var val = Input.GetAxis("Vertical");
            AddForce(Vector3.forward * val, out isMoving);
        }

        if (Input.GetButtonDown("Artifact 1") || Input.GetButtonDown("Artifact 2"))
        {
            if (GetComponent<OmniSlashAbility>())
            {
                animator.ResetTrigger("TempestTrigger");
                animator.SetTrigger("TempestTrigger");
            }
        }

        AnimateMovement();
        PlayMovementSound(isMoving);
    }

    public void EnableInputs()
    {
        movementEnabled = true;
    }

    public void DisableInputs()
    {
        movementEnabled = false;
    }
    
    public async void TempDisablePlayerInputs()
    {
        DisableInputs();
        await new WaitForSeconds(10);
        EnableInputs();
    }

    public bool GetNearStation()
    {
        return isAtCookingStation;
    }

    private void AnimateMovement()
    {
        animator.SetFloat("MoveX", rb.velocity.x);
        animator.SetFloat("MoveY", rb.velocity.z);

        spriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, rb.velocity.x > 0 ? 180f : 0f, 0));
    }
    private void PlayMovementSound(bool status)
    {
        audioScript.OnPlayerMove(status);
    }

    public void Attack()
    {
        canAttack = false;

        attackCollider.radius = playerUnitData.AttackRange;

        var (attacked, counter) = jab.Attack();

        if (!attacked) return;
        
        if (counter == 1) attackParticle.Play();
        var variation = counter == 1 ? "" : counter.ToString();
        animator.ResetTrigger($"Attack Finish{variation}");
        animator.SetTrigger($"Attack Start{variation}");
        PlayRandomAttackSound();
    }

    private void PlayRandomAttackSound()
    {
        if (ynahAttackSound == null || ynahAttackSound.Count == 0)
            return;

        int randomSoundIndex = Random.Range(0, ynahAttackSound.Count);

        AudioClip randomSlash = ynahAttackSound[randomSoundIndex];

        ynahAttackAudio.PlayOneShot(randomSlash);
    }

    public PlayerInventory GetInventory()
    {
        return inventory;
    }

    public PlayerInventory Swap(PlayerInventory newInv)
    {
        var i = inventory;
        inventory = newInv;
        return i;
    }

    public PlayerUnitData GetPlayerData()
    {
        return playerUnitData;
    }
}
