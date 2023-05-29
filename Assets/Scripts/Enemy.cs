using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Unit Data")]
    [SerializeField] private EnemyUnitData enemyUnitData;

    private EnemyUnitData enemyDataInstance;
    [SerializeField] private SphereCollider aggroTrigger;
    [SerializeField] private GameObject targetUnit;

    private bool isAttacking = false;
    private bool canAttack = true;

    [Header("SFX")]
    public Material redMaterial;
    public Material greenMaterial;
    

    [Header("Health UI")]
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyDataInstance = new EnemyUnitData();

        enemyDataInstance = ScriptableObject.CreateInstance<EnemyUnitData>(); 
        enemyDataInstance.MaxHealth = enemyUnitData.MaxHealth;
        enemyDataInstance.CurrentHealth = enemyDataInstance.MaxHealth;
        enemyDataInstance.UnitName = enemyUnitData.UnitName;
        enemyDataInstance.MoveSpeed = enemyUnitData.MoveSpeed;
        enemyDataInstance.Drop = enemyUnitData.Drop;
        enemyDataInstance.AggroRange = enemyUnitData.AggroRange;

        aggroTrigger.radius = enemyDataInstance.AggroRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            targetUnit = other.gameObject;
            aggroTrigger.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float a = enemyDataInstance.CurrentHealth;
        float b = enemyDataInstance.MaxHealth;
        float normalized = a / b;

        hpBar.fillAmount = normalized;

        if (targetUnit)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);
            if (distanceToTarget > 20) 
            { 
                ResetAggro(); 
            }
            else if (distanceToTarget <= 5 && canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void ResetAggro()
    {
        targetUnit = null;
        aggroTrigger.enabled = true;
        isAttacking = false;
    }

    IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        yield return new WaitForSeconds(3.0f);

        if (targetUnit)
        {  
            float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);

            if (distanceToTarget < 5)
            {
                targetUnit.GetComponent<Player>().TakeDamage(1);
            }
        }
        canAttack = true;
    }

    public void TakeDamage(int damageValue)// Move to a seperate damage handler maybe
    {
        enemyDataInstance.CurrentHealth -= damageValue;
        StartCoroutine(Hit());

        if (enemyDataInstance.CurrentHealth <= 0)
        {
            Death();
        }
    }

    IEnumerator Hit()// To be replaced by animations
    {
        GetComponentInChildren<Renderer>().material = redMaterial;
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Renderer>().material = greenMaterial;
    }

    void Death()
    {
        Instantiate(enemyDataInstance.Drop, transform.position, Quaternion.identity);
        
        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }
}
