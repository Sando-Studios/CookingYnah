using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;
using UnityEngine.AI;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    [Header("Unit Data")]
    [Tooltip("Assign in inspector ONLY if this object will be one of the Mini-bosses")]
    [SerializeField] protected BossUnitData bossDataInstance;
    protected EnemyUnitData enemyDataInstance;
    private float maxHealth;

    protected GameObject targetUnit;

    [Header("Health UI")]
    public Image hpBar;

    protected bool canAttack = true;
    protected bool isAttackDone = false;
    protected Vector3 home;
    protected bool isAlive = true;

    [Header("Animation")]
    [SerializeField] protected Transform spriteTransform;
    protected Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    public SerializedDictionary<string, AudioClip> nameClipDictionary = new SerializedDictionary<string, AudioClip>();

    protected NavMeshAgent agent;

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public virtual bool IsAlive()
    {
        return isAlive;
    }
    public virtual bool GetCanAttack()
    {
        return canAttack;
    }
    public virtual void SetCanAttack(bool isAttackPossible)
    {
        canAttack = isAttackPossible;
    }
    public virtual bool GetIsAttackDone()
    {
        return isAttackDone;
    }
    public virtual void SetIsAttackDone(bool hasAttackFinished)
    {
        isAttackDone = hasAttackFinished;
    }

    public virtual GameObject GetTargetUnit()
    {
        return targetUnit;
    }
    public virtual UnitData GetUnitData()
    {
        if (bossDataInstance != null) return bossDataInstance;
        //if (enemyDataInstance != null) return enemyDataInstance;

        return enemyDataInstance;
    }

    protected abstract void Death(int id);

    protected abstract void Death(Artifacts artifact, string name);

    protected virtual async void AttackTimer(float attackSpeed)
    {
        await new WaitForSeconds(attackSpeed);
        SetCanAttack(true);
    }

    protected virtual void Start()
    {
        animator = spriteTransform.GetComponent<Animator>();

        if (enemyDataInstance != null)
            maxHealth = enemyDataInstance.MaxHealth;
        else if (bossDataInstance != null)
            maxHealth = bossDataInstance.MaxHealth;

        agent = GetComponent<NavMeshAgent>();

    }
    protected virtual void Update()
    {
        if (!GetUnitData())
            return;

        float currentHP = GetUnitData().CurrentHealth;

        float normalized = currentHP / maxHealth;

        hpBar.fillAmount = normalized;

        if (agent.hasPath)
        {
            Vector3 direction = agent.velocity.normalized;

            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));
        }
    }

    public virtual async void Hit()
    {
        var r = GetComponentsInChildren<SpriteRenderer>();

        PlayAudioClip(GetAudioClipName("Hurt"));

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

    protected string GetAudioClipName(string inputName)
    {
        Debug.Log("GetAudioClipName()");
        if (nameClipDictionary.ContainsKey(inputName))
        {
            Debug.Log("Exact Name");
            return inputName;
        }

        List<string> matchingClipNames = new List<string>();

        foreach (var kvp in nameClipDictionary)
        {
            if (kvp.Key.Contains(inputName))
            {
                matchingClipNames.Add(kvp.Key);
            }
        }

        if (matchingClipNames.Count > 0)
        {
            Debug.Log("Contains Name");
            int randomIndex = UnityEngine.Random.Range(0, matchingClipNames.Count);
            return matchingClipNames[randomIndex];
        }

        Debug.Log("nothing");
        return "THere is nothing there";
    }

    protected void PlayAudioClip(string name)
    {
        Debug.Log("PlayAudioClip()");
        if (nameClipDictionary.ContainsKey(name))
            audioSource.clip = nameClipDictionary[name];

        if (audioSource.clip.Equals(nameClipDictionary[name]))
        {
            audioSource.Play();
            Debug.Log("Play()");
        }
        else
            Debug.Log("THE FUCK YOU LOOKING FOR"); Debug.Log("else");
    }
}
