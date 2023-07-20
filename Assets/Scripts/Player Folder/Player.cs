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

    private bool isAtCookingStation = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem attackParticle;

    private bool movementEnabled = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        inventory = GetComponent<PlayerInventory>();

        UIManager.instance.UpdateHpUI();
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

        if (Input.GetButton("Horizontal"))
        {
            var val = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(val, 0, 0) * force * Time.deltaTime, ForceMode.Force);
        }

        if (Input.GetButton("Vertical"))
        {
            var val = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(0, 0, val) * force * Time.deltaTime, ForceMode.Force);
        }

        AnimateMovement();
    }

    public void EnableInputs()
    {
        movementEnabled = true;
    }

    public void DisableInputs()
    {
        movementEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Cook Station"))
        {
            isAtCookingStation = true;
            UIManager.instance.SetCraftingPopUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Cook Station"))
        {
            isAtCookingStation = false;
            UIManager.instance.SetCraftingPopUp();
        }
    }

    public bool GetNearStation()
    {
        return isAtCookingStation;
    }

    private void AnimateMovement()
    {
        animator.SetFloat("MoveX", rb.velocity.x);
        animator.SetFloat("MoveY", rb.velocity.z);

        transform.rotation = Quaternion.Euler(new Vector3(0, rb.velocity.x > 0 ? 180f : 0f, 0));
    }

    private void Attack()
    {
        canAttack = false;

        attackCollider.radius = playerUnitData.AttackRange;

        var (attacked, isSlow) = jab.Attack();
        
        if (!attacked) return;

        if (!isSlow)
        {
            attackParticle.Play();
            animator.ResetTrigger("Attack Finish");
            animator.SetTrigger("Attack Start");
        }
        else
        {
            throw new NotImplementedException("No slow attack animation yet");
        }
    }

    public PlayerInventory GetInventory()
    {
        return inventory;
    }
    public PlayerUnitData GetPlayerData()
    {
        return playerUnitData;
    }
}
