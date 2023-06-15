using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class Player : MonoBehaviour
{   
    [Header("Unit DropData")]
    [SerializeField] private PlayerUnitData playerUnitData;
    private PlayerUnitData playerDataInstance;
    private Rigidbody rb;
    public float force;

    [Header("OmniAttack")]
    [SerializeField] private OmniAttack attack;
    [SerializeField] private SphereCollider attackCollider;
    private bool canAttack = true;

    [Header("Inventory")]
    [SerializeField] private PlayerInventory inventory;

    private bool isAtCookingStation = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem attackParticle;

    private void Awake()
    {
        
        playerDataInstance = ScriptableObject.CreateInstance<PlayerUnitData>();
        SetInitialValues();
        
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        inventory = GetComponent<PlayerInventory>();

        UIManager.instance.UpdateHpUI();
        BuffManager.instance.SetPlayer(playerDataInstance);

        if (SceneChangeManager.instance.GetObjectToLoad() != gameObject) { Destroy(gameObject); }
    }

    void SetInitialValues()
    {

        playerDataInstance.UnitName = playerUnitData.UnitName;

        playerDataInstance.Vitality = playerUnitData.Vitality;
        playerDataInstance.Agility = playerUnitData.Agility;
        playerDataInstance.Strength = playerUnitData.Strength;
        playerDataInstance.Vigor = playerUnitData.Vigor;
        playerDataInstance.Intelligence = playerUnitData.Intelligence;
        playerDataInstance.Endurance = playerUnitData.Endurance;
        playerDataInstance.Dexterity = playerUnitData.Dexterity;
        playerDataInstance.MoveSpeed = playerUnitData.MoveSpeed;
        playerDataInstance.MaxHealth = playerUnitData.MaxHealth;
        playerDataInstance.CurrentHealth = playerDataInstance.MaxHealth;
        playerDataInstance.AttackRange = playerUnitData.AttackRange;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            //attackCollider.enabled = true;
            Attack();

            animator.SetTrigger("attackTrigger");
        }


        if (Input.GetButton("Horizontal"))
        {
            var val = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(val, 0, 0) * force, ForceMode.Force);
        }

        if (Input.GetButton("Vertical"))
        {
            var val = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(0, 0, val) * force, ForceMode.Force);
        }

        AnimateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Cook Station")
        {
            isAtCookingStation = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Cook Station")
        {
            isAtCookingStation = false;
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

    private async void Attack()
    {
        attackParticle.Play();
        canAttack = false;

        attackCollider.radius = playerDataInstance.AttackRange;
        attack.DealDamage((int)playerDataInstance.RawDamage);
<<<<<<< HEAD

        // Debug.Log($"{playerUnitData.AttackInterval}");
        await new WaitForSeconds(playerUnitData.AttackInterval);

        // Debug.Log("done attacking");
=======
        
        await new WaitForSeconds(playerUnitData.AttackInterval);

>>>>>>> 200b192714eafb6504647639ce679941591ca5a3
        animator.SetTrigger("attackTrigger");
        canAttack = true;
    }


    public PlayerInventory GetInventory()
    {
        return inventory;
    }
    public PlayerUnitData GetPlayerData()
    {
        return playerDataInstance;
    }
}
