using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;

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

    private void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += Death;
    }
    private void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= Death;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyDataInstance = new EnemyUnitData();

        SetData(10001); // To be called and set by spawner 
    }

    public void SetData(int enemyID)
    {
        //enemyDataInstance = ScriptableObject.CreateInstance<EnemyUnitData>();
        enemyDataInstance.UnitID = enemyID;
        enemyDataInstance.MaxHealth = enemyUnitData.MaxHealth;
        enemyDataInstance.CurrentHealth = enemyDataInstance.MaxHealth;
        enemyDataInstance.UnitName = enemyUnitData.UnitName;
        enemyDataInstance.MoveSpeed = enemyUnitData.MoveSpeed;
        enemyDataInstance.Drop = enemyUnitData.Drop;
        enemyDataInstance.Data = enemyUnitData.Data;
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
                Attack();
            }
        }
    }

    void ResetAggro()
    {
        targetUnit = null;
        aggroTrigger.enabled = true;
        isAttacking = false;
    }

    private async void Attack()
    {
        canAttack = false;
        isAttacking = true;

        await new WaitForSeconds(3.0f);

        if (targetUnit)
        {  
            float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);

            if (distanceToTarget < 5)
            {
                DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), 1);
            }
        }
        canAttack = true;
    }

    public EnemyUnitData GetEnemyUnitData()
    {
        return enemyDataInstance;
    }

    public async void Hit()// To be replaced by animations and converted to a trigger
    {
        GetComponentInChildren<Renderer>().material = redMaterial;
        await new WaitForSeconds(0.5f);
        GetComponentInChildren<Renderer>().material = greenMaterial;
    }

    void Death(int id)
    {
        if(id != enemyDataInstance.UnitID) { return; }

        GameObject clone = Instantiate(enemyDataInstance.Drop, transform.position, Quaternion.identity);
        clone.GetComponent<Item>().SetData(enemyDataInstance.Data);

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }
}
