using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyUnitData enemyUnitData;

    private EnemyUnitData enemyDataInstance;
    [SerializeField] private SphereCollider aggroTrigger;
    [SerializeField] private GameObject targetUnit;

    private bool isAttacking = false;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyDataInstance = new EnemyUnitData();
        enemyDataInstance.Health = enemyUnitData.Health;
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
        if (targetUnit)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);
            if (distanceToTarget > 30) 
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

        yield return new WaitForSeconds(3.0f);

        float distanceToTarget = Vector3.Distance(transform.position, targetUnit.transform.position);

        if (distanceToTarget < 10)
        {
            targetUnit.GetComponent<Player>().TakeDamage(1);
        }

        canAttack = true;
    }
}
