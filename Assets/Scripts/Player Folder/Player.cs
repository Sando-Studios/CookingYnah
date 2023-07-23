using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Update is called once per frame
    void Update()
    {

        // TODO: Synchronize animations, since canAttack got replaced
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (!movementEnabled) return;

        bool isMoving = false;

        if (Input.GetButton("Horizontal"))
        {
            var val = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(val, 0, 0) * force * Time.deltaTime, ForceMode.Force);

            isMoving = true;
        }

        if (Input.GetButton("Vertical"))
        {
            var val = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(0, 0, val) * force * Time.deltaTime, ForceMode.Force);

            isMoving = true;
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

    private void Attack()
    {
        canAttack = false;

        attackCollider.radius = playerUnitData.AttackRange;

        var (attacked, counter) = jab.Attack();

        if (!attacked) return;
        
        switch (counter)
        {
            case 0:
                break;
            case 1:
                attackParticle.Play();
                animator.ResetTrigger("Attack Finish");
                animator.SetTrigger("Attack Start");
                break;
            case 2:
                //throw new NotImplementedException("No second attack animation yet");
                animator.ResetTrigger("Attack Finish");
                animator.SetTrigger("Attack Start2");
                break;
            case 3:
                //throw new NotImplementedException("No slow attack animation yet");
                animator.ResetTrigger("Attack Finish");
                animator.SetTrigger("Attack Start3");
                break;
            default:
                Debug.LogError("HOW");
                break;
        }
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
