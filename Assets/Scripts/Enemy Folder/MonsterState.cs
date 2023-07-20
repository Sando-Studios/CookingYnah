using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterState
{
    protected NavMeshAgent agent;

    protected MonsterStateManager statManager;
    protected MinorEnemy enemy;

    public MonsterState(MonsterStateManager manager, MinorEnemy enemy)
    {
        statManager = manager;
        this.enemy = enemy;
    }

    public virtual void Enter() { }

    public virtual void Update(float deltaTime) { }

    public virtual void Exit() { }
}
