using System;
using System.Collections;
using UnityEngine;
using Asyncoroutine;
using UnityEngine.AI;

public class MinorEnemy : Enemy
{
    protected override void Death(Artifacts artifact, string name) { Debug.Log("yoooooooooooooo"); }
    
    [SerializeField] protected SphereCollider aggroTrigger;

    protected virtual void OnEnable()
    {
        DamageHandler.OnEnemyUnitDeath += Death;
    }

    protected virtual void OnDisable()
    {
        DamageHandler.OnEnemyUnitDeath -= Death;
    }

    [Header("Health UI")]
    public GameObject hpBarGameObject;
    protected Coroutine hpBarCoroutine;

    private float randomInterval;
    private float time;

    public virtual void SetEnemyData(int enemyID, EnemyUnitData unitData, Vector3 homeBase)
    {
        enemyDataInstance = ScriptableObject.CreateInstance<EnemyUnitData>();

        home = homeBase;
        enemyDataInstance.Init(enemyID, unitData);
        aggroTrigger.radius = enemyDataInstance.AggroRange;
   
    }

    protected override void Start()
    {
        base.Start();
        
        if(MonsterStateManager.Instance.GetIsAiActive())
            MonsterStateManager.Instance.AddMonster(this, new PatrolState(MonsterStateManager.Instance, this));
        else
        {
            MonsterStateManager.Instance.AddMonster(this, new IdleState(MonsterStateManager.Instance, this));
        }
    }

    public EnemyUnitData GetEnemyData()
    {
        return GetUnitData() as EnemyUnitData;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            targetUnit = other.gameObject;
            aggroTrigger.enabled = false;
        }
    }

    public virtual void ResetAggro()
    {
        targetUnit = null;
        aggroTrigger.enabled = true;
    }

    public virtual Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = home + UnityEngine.Random.insideUnitSphere * 7.0f;
        NavMesh.SamplePosition(randomPoint, out NavMeshHit point, 7.0f, NavMesh.AllAreas);
        return point.position;
    }

    public override async void Hit() 
    {
        base.Hit();
        ShowHPBar();

        await new WaitForSeconds(0);
    }

    protected override void Death(int id)
    {
        if (id != enemyDataInstance.UnitID) { return; }

        isAlive = false;

        ItemData data = GetRandomItemData();

        GameObject clone = Instantiate(enemyDataInstance.DropObject, transform.position, Quaternion.identity);
        clone.GetComponent<Item>().SetData(data);

        transform.GetComponent<NavMeshAgent>().enabled = false;

        PlayAudioClip(GetAudioClipName("Death"));

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }

    protected virtual ItemData GetRandomItemData()
    {
        float totalWeight = 0f;
        foreach (float dropChance in enemyDataInstance.DropData.Values)
        {
            totalWeight += dropChance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        ItemData data = null;
        foreach (var entry in enemyDataInstance.DropData)
        {
            randomValue -= entry.Value;
            if (randomValue <= 0f)
            {
                data = entry.Key;
                break;
            }
        }

        return data;
    }

    public virtual void ExecuteAttack()
    {
        if (targetUnit)
        {
            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.Normalize();
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            AttackTimer(enemyDataInstance.AttackSpeed);
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), enemyDataInstance.BasicAttackDamage);
        }
    }

    public virtual void ControlAnimations(MonsterStates state, bool isPlaying)
    {
        if(!animator)
            Debug.Log("no animator");
        
        ResetAnimatorBool();

        var s = state;
        switch (s)
        {
            case MonsterStates.Attack:
                animator.SetBool("isAttacking", isPlaying);
                break;
            case MonsterStates.Combat:
                animator.SetBool("isInCombat", isPlaying);
                break;
            case MonsterStates.Chase:
                animator.SetBool("isChasing", isPlaying);
                break;
            case MonsterStates.Idle:
                animator.SetBool("isIdling", isPlaying);
                break;
            case MonsterStates.Patrol:
                animator.SetBool("isPatrolling", isPlaying);
                break;

        }
    }

    protected virtual void ResetAnimatorBool()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isInCombat", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isIdling", false);
        animator.SetBool("isPatrolling", false);
    }

    public void ShowHPBar()
    {
        if (hpBarCoroutine != null)
        {
            StopCoroutine(hpBarCoroutine);
        }

        hpBarGameObject.SetActive(true);
        hpBarCoroutine = StartCoroutine(HideHPBarAfterDelay());
    }

    protected IEnumerator HideHPBarAfterDelay()
    {
        yield return new WaitForSeconds(3.0f);
        hpBarGameObject.SetActive(false);
    }

    public void PlaySoundRandomTime(string clipName)
    {
        time += Time.deltaTime;

        if (time >= randomInterval)
        {
            randomInterval = UnityEngine.Random.Range(1, 10);
            PlaySound(clipName);
            time = 0f;
        } 
    }

    public void PlaySound(string clipName)
    {
        PlayAudioClip(GetAudioClipName(clipName));
    }
}
