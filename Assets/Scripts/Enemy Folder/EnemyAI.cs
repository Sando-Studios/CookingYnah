using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private MonsterStateMachine stateMachine;
    private bool isActive = false;

    private void Start()
    {
        stateMachine = ScriptableObject.CreateInstance<MonsterStateMachine>();
    }

    public void StartAI()
    {
        stateMachine.Initialize(enemy);
        FlipIsActive();
    }

    public void FlipIsActive()
    {
        isActive = !isActive;
    }

    private void Update()
    {
        if (isActive)
        {
            stateMachine.UpdateState();
        }

    }
}
