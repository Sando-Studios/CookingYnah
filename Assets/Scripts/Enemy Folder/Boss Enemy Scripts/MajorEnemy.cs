using Asyncoroutine;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Idle,
    BasicAttack,
    SpecialAttack,
    InCombat,
    Chase
}
public class MajorEnemy : Enemy
{
    protected NavMeshAgent agent;
    private BossState currentState;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        TransitionToState(BossState.Idle);
    }

    public BossUnitData GetBossData()
    {
        return GetUnitData() as BossUnitData;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            targetUnit = other.gameObject;
            //aggroTrigger.enabled = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float a = bossDataInstance.CurrentHealth;
        float b = bossDataInstance.MaxHealth;
        float normalized = a / b;

        hpBar.fillAmount = normalized;

        if (agent.hasPath)
        {
            Vector3 direction = agent.velocity.normalized;

            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));
        }

        // Handle state-specific behavior in the Update method
        switch (currentState)
        {
            case BossState.Idle:
                IdleBehavior();
                break;
            case BossState.BasicAttack:
                BasicAttackBehavior();
                break;
            case BossState.SpecialAttack:
                SpecialAttackBehavior();
                break;
            case BossState.InCombat:
                InCombat();
                break;
            case BossState.Chase:
                Chase();
                break;
        }
    }

    public void IdleBehavior()
    {

    }
    public void BasicAttackBehavior()
    {

    }
    public void SpecialAttackBehavior()
    {

    }
    public void InCombat()
    {

    }
    public void Chase()
    {

    }


    public virtual async void Hit()
    {
        var r = GetComponentsInChildren<SpriteRenderer>();

        foreach (var m in r)
        {
            m.color = Color.red;
        }
        await new WaitForSeconds(0.5f);

        foreach (var m in r)
        {
            m.color = new Color(255, 255, 255, 255);
        }
    }

    protected override void Death(Artifacts artifact, string name)
    {
        if (name != bossDataInstance.UnitName) { return; }

        isAlive = false;

        transform.GetComponent<NavMeshAgent>().enabled = false;

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
    }

    public virtual void ExecuteBasicAttack()
    {
        if (targetUnit)
        {
            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.Normalize();
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            AttackTimer(bossDataInstance.BasicAttackSpeed);
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), bossDataInstance.BasicAttackDamage);
        }
    }

    public virtual void ExecuteSpecialAttack()
    {
        if (targetUnit)
        {

        }
    }



    public virtual void ControlAnimations(BossState state, bool isPlaying)
    {
        ResetAnimatorBool();

        var s = state;
        switch (s)
        {
            case BossState.Idle:
                animator.SetBool("isIdling", isPlaying);
                break;
            case BossState.BasicAttack:
                animator.SetBool("isBasicAttacking", isPlaying);
                break;
            case BossState.SpecialAttack:
                animator.SetBool("isSpecialAttacking", isPlaying);
                break;
            case BossState.InCombat:
                animator.SetBool("isInCombat", isPlaying);
                break;
            case BossState.Chase:
                animator.SetBool("isChasing", isPlaying);
                break;
        }
    }

    protected virtual void ResetAnimatorBool()
    {
        animator.SetBool("isIdling", false);
        animator.SetBool("isBasicAttacking", false);
        animator.SetBool("isSpecialAttacking", false);
        animator.SetBool("isInCombat", false);
        animator.SetBool("isChasing", false); ;
    }

    public void TransitionToState(BossState newState)
    {
        // Exit the current state
        ExitState();

        // Enter the new state
        currentState = newState;
        EnterState();
    }

    private void EnterState()
    {
        // Perform actions when entering a new state
        switch (currentState)
        {
            case BossState.Idle:
                ControlAnimations(currentState, true);
                break;
            case BossState.BasicAttack:
                ControlAnimations(currentState, true);
                break;
            case BossState.SpecialAttack:
                ControlAnimations(currentState, true);
                break;
            case BossState.InCombat:
                ControlAnimations(currentState, true);
                break;
            case BossState.Chase:
                ControlAnimations(currentState, true);
                break;
        }
    }

    private void ExitState()
    {
        // Perform actions when exiting the current state
        switch (currentState)
        {
            case BossState.Idle:
                ControlAnimations(currentState, false);
                break;
            case BossState.BasicAttack:
                ControlAnimations(currentState, false);
                break;
            case BossState.SpecialAttack:
                ControlAnimations(currentState, false);
                break;
            case BossState.InCombat:
                ControlAnimations(currentState, false);
                break;
            case BossState.Chase:
                ControlAnimations(currentState, false);
                break;
        }
    }
}
