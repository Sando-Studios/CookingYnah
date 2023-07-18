using Asyncoroutine;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public enum BossState
{
    Idle,
    BasicAttack,
    SpecialAttack,
    InCombat,
    Stunned,
    Chase,
    Death
}
public class MajorEnemy : Enemy
{
    [SerializeField] private TextMeshProUGUI bossNameText;
    private BossState currentState;

    private bool isPlayerInRoom = false;
    private Vector3 targetPos;

    protected override void Start()
    {
        base.Start();
        home = transform.position;
        TransitionToState(BossState.Idle);
    }

    public BossUnitData GetBossData()
    {
        return GetUnitData() as BossUnitData;
    }

    public void SetPlayerStatus(bool isPlayerInArea, GameObject playerObj)
    {
        isPlayerInRoom = isPlayerInArea;
        targetUnit = playerObj;
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
    protected override void Update()
    {
        base.Update();

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
                InCombatBehavior();
                break;
            case BossState.Stunned:
                StunnedBehavior();
                break;
            case BossState.Chase:
                ChaseBehavior();
                break;
        }
    }

    public void IdleBehavior()
    {
        if (isPlayerInRoom)
        {
            TransitionToState(BossState.Chase);
            return;
        }
    }
    public void ChaseBehavior()
    {
        if (isPlayerInRoom) targetPos = targetUnit.transform.position;
        else if (!isPlayerInRoom) targetPos = home;

        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        if (isPlayerInRoom)
        {
            if (distanceToTarget <= bossDataInstance.AttackRange)
            {
                TransitionToState(BossState.InCombat);
                return;
            }
            else
            {
                agent.SetDestination(targetUnit.transform.position);
            }
        }
        else
        {
            if (distanceToTarget <= 0.1f)
            {
                TransitionToState(BossState.Idle);
                return;
            }
            else
            {
                agent.SetDestination(home);
                return;
            }
        }
    }

    private int attackCount = 0;

    public void InCombatBehavior()
    {
        float distanceToTarget = Vector3.Distance(transform.position, GetTargetUnit().transform.position);

        if (distanceToTarget > bossDataInstance.AttackRange)
        {
            TransitionToState(BossState.Chase);
            return;
        }
        else if (distanceToTarget <= bossDataInstance.AttackRange && GetCanAttack())
        {
            attackCount++;

            if (attackCount % 4 == 0)
            {
                attackCount = 0;
                TransitionToState(BossState.SpecialAttack);
                return;
            }
            else
            {
                TransitionToState(BossState.BasicAttack);
                return;
            }
        }

    }

    public void BasicAttackBehavior()
    {
        if (GetCanAttack())
        {
            StartAttack();
            ExecuteBasicAttack();
        }

        if (!GetCanAttack() && GetIsAttackDone())
        {
            TransitionToState(BossState.InCombat);
            return;
        }
    }
    public void SpecialAttackBehavior()
    {
        if (GetCanAttack())
        {
            StartAttack();
            ExecuteSpecialAttack();
        }

        if (!GetCanAttack() && GetIsAttackDone())
        {
            TransitionToState(BossState.Stunned);
            return;
        }
    }

    private void StartAttack()
    {
        SetCanAttack(false);
        SetIsAttackDone(false);
    }

    private async void StunnedBehavior()
    {
        await new WaitForSeconds(bossDataInstance.StunnedDuration);

        TransitionToState(BossState.InCombat);
    }

    public virtual void ExecuteBasicAttack()
    {
        if (targetUnit)
        {
            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.Normalize();
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            AttackTimer(bossDataInstance.BasicAttackSpeed);
            SetIsAttackDone(true);
            //DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), bossDataInstance.BasicAttackDamage);
        }
    }

    public virtual void ExecuteSpecialAttack()
    {
        if (targetUnit)
        {
            AttackTimer(bossDataInstance.BasicAttackSpeed);
            OmniSlashAbility superSlash
            SetIsAttackDone(true);
        }
    }
    protected override void Death(Artifacts artifact, string name)
    {
        if (name != bossDataInstance.UnitName) { return; }

        TransitionToState(BossState.Death);

        isAlive = false;

        transform.GetComponent<NavMeshAgent>().enabled = false;

        Vector3 position = transform.position;
        position.y -= 40f;
        transform.position = position;

        Destroy(gameObject, 3.0f);
        bossDataInstance.SetHealthToDefault();
    }

    public virtual void ControlAnimations(BossState state, bool isPlaying)
    {
        ResetAnimatorBool();

        var s = state;
        switch (s)
        {
            case BossState.Idle:
                //animator.SetBool("isIdling", isPlaying);
                break;
            case BossState.BasicAttack:
                //animator.SetBool("isBasicAttacking", isPlaying);
                break;
            case BossState.SpecialAttack:
                //animator.SetBool("isSpecialAttacking", isPlaying);
                break;
            case BossState.InCombat:
                //animator.SetBool("isInCombat", isPlaying);
                break;
            case BossState.Stunned:
                //animator.SetBool("isStunned", isPlaying);
                break;
            case BossState.Chase:
                //animator.SetBool("isChasing", isPlaying);
                break;
        }
    }

    protected virtual void ResetAnimatorBool()
    {
        //animator.SetBool("isIdling", false);
        //animator.SetBool("isBasicAttacking", false);
        //animator.SetBool("isSpecialAttacking", false);
        //animator.SetBool("isInCombat", false);
        //animator.SetBool("isStunned", false);
        //animator.SetBool("isChasing", false); ;
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
            case BossState.Stunned:
                ControlAnimations(currentState, true);
                break;
            case BossState.Chase:
                agent.stoppingDistance = bossDataInstance.AttackRange - 1;
                ControlAnimations(currentState, true);
                break;
            case BossState.Death:
                ResetAnimatorBool();
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
            case BossState.Stunned:
                ControlAnimations(currentState, false);
                break;
            case BossState.Chase:
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                ControlAnimations(currentState, false);
                break;
        }
    }
}
