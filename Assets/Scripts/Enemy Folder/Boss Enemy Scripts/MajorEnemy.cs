using System;
using Asyncoroutine;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
    protected override void Death(int id)
    {
        throw new InvalidOperationException("This should never happen btw");
    }

    [Header("Boss Name")]
    [SerializeField] private TextMeshProUGUI bossNameText;
    private BossState currentState;

    private bool isPlayerInRoom = false;
    private Vector3 targetPos;

    private float randomInterval;
    private float time;

    protected override void OnEnable()
    {
        DamageHandler.OnBossUnitDeath += Death;
    }

    protected override void OnDisable()
    {
        DamageHandler.OnBossUnitDeath -= Death;
    }

    protected override void Start()
    {
        base.Start();
        bossNameText.text = bossDataInstance.UnitName;
        bossNameText.gameObject.SetActive(false);
        hpBar.transform.parent.gameObject.SetActive(false);
        home = transform.position;
        bossDataInstance.SetHealthToDefault();
        randomInterval = UnityEngine.Random.Range(1, 7);
        TransitionToState(BossState.Idle);
    }

    public BossUnitData GetBossData()
    {
        return GetUnitData() as BossUnitData;
    }
    public BossState GetBossState() { return currentState; }

    public void SetPlayerStatus(bool isPlayerInArea, GameObject playerObj)
    {
        isPlayerInRoom = isPlayerInArea;
        targetUnit = playerObj;
        bossNameText.gameObject.SetActive(isPlayerInArea);
        hpBar.transform.parent.gameObject.SetActive(isPlayerInArea);
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
            case BossState.Death:
                break;
        }
    }

    private void IdleBehavior()
    {
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

        if (isPlayerInRoom)
        {
            TransitionToState(BossState.Chase);
            return;
        }
    }
    private void ChaseBehavior()
    {
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

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
                time += Time.deltaTime;

                if (time >= randomInterval)
                {
                    PlayAudioClip(GetAudioClipName("Chase"));
                    time = 0f;
                }

                agent.SetDestination(home);
                return;
            }
        }
    }

    private int attackCount = 0;

    private void InCombatBehavior()
    {
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, GetTargetUnit().transform.position);

        if (distanceToTarget > bossDataInstance.AttackRange)
        {
            TransitionToState(BossState.Chase);
            return;
        }
        else if (distanceToTarget <= bossDataInstance.AttackRange && GetCanAttack())
        {

            time += Time.deltaTime;

            if (time >= randomInterval)
            {
                PlayAudioClip(GetAudioClipName("Combat"));
                time = 0f;
            }

            if (attackCount % 4 == 0 && attackCount > 0)
            {
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

    private void BasicAttackBehavior()
    {
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

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
    private void SpecialAttackBehavior()
    {
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

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
        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }
        
        PlayAudioClip(GetAudioClipName("Stun"));

        await new WaitForSeconds(bossDataInstance.StunnedDuration);

        if (!isAlive)
        {
            TransitionToState(BossState.Death);
            return;
        }

        TransitionToState(BossState.InCombat);
    }

    public virtual void ExecuteBasicAttack()
    {
        Vector3 direction = targetUnit.transform.position - transform.position;
        direction.Normalize();
        spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

        AttackTimer(bossDataInstance.BasicAttackSpeed);

        PlayAudioClip(GetAudioClipName("BasicA"));
    }

    public void CheckBasicAttackHit()
    {
        float distanceToTarget = Vector3.Distance(transform.position, GetTargetUnit().transform.position);

        if (distanceToTarget <= bossDataInstance.AttackRange)
        {
            DamageHandler.ApplyDamage(targetUnit.GetComponent<Player>(), bossDataInstance.BasicAttackDamage);
        }
    }

    public virtual void ExecuteSpecialAttack()
    {
        AttackTimer(bossDataInstance.SpecialAttackSpeed);
        OmniSlashAbility omniSlash = GetComponent<OmniSlashAbility>();
        omniSlash.SpawnBossSlashZone(bossDataInstance.SpecialAttackDamage);

        PlayAudioClip(GetAudioClipName("SpecialA"));

        SetIsAttackDone(true);
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

    public void AddToAttackCount(int value)
    {
        attackCount += value;
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
            case BossState.Stunned:
                animator.SetBool("isStunned", isPlaying);
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
        animator.SetBool("isStunned", false);
        animator.SetBool("isChasing", false);
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
                attackCount = 0;
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
            default:
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
                ControlAnimations(currentState, false);
                break;
            default:
                break;
        }
    }
}
