using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Unit Data")]
    [SerializeField] private PlayerUnitData playerUnitData;
    private PlayerUnitData playerDataInstance;
    private Rigidbody rb;
    public float force;
    
    [SerializeField] private GameObject targetUnit;

    private bool isAttacking = false;
    private bool canAttack = true;

    [Header("SFX")]
    public Material redMaterial;
    public Material greenMaterial;

    [Header("Invetory")]
    [SerializeField] private PlayerInventory invetory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerDataInstance = new PlayerUnitData();
        SetInitalValues();
    }

    void SetInitalValues()
    {
        playerDataInstance.MaxHealth = playerUnitData.MaxHealth;
        playerDataInstance.CurrentHealth = playerDataInstance.MaxHealth;
        playerDataInstance.UnitName = playerUnitData.UnitName;
        playerDataInstance.MoveSpeed = playerUnitData.MoveSpeed;
        playerDataInstance.Vitality = playerUnitData.Vitality;
        playerDataInstance.Agility = playerUnitData.Agility;
        playerDataInstance.Strength = playerUnitData.Strength;
        playerDataInstance.Vigor = playerUnitData.Vigor;
        playerDataInstance.Intelligence = playerUnitData.Intelligence;
        playerDataInstance.Endurance = playerUnitData.Endurance;
        playerDataInstance.Dexterity = playerUnitData.Dexterity;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetUnit)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canAttack && targetUnit && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine(Attack());
            }
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            targetUnit = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            targetUnit = null;
        }
    }

    public void TakeDamage(int damageValue)// Move to a seperate damage handler maybe
    {
        playerDataInstance.CurrentHealth -= damageValue;
        StartCoroutine(Hit());
    }

    IEnumerator Attack()
    {
        canAttack = false;
        
        if (targetUnit)
        {
            targetUnit.GetComponent<Enemy>().TakeDamage(1);
        }
        yield return new WaitForSeconds(3.0f);

        isAttacking = false;
        canAttack = true;
    }

    IEnumerator Hit()// To be replaced by animations
    {
        GetComponent<Renderer>().material = redMaterial;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material = greenMaterial;
    }

    public PlayerInventory GetInventory()
    {
        return invetory;
    }
}
