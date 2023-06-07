using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MonsterStates
{
    Idle,
    Attack,
    Chase,
    Patrol,
    Dead
}
public class MonsterStateManager : MonoBehaviour
{
    private static MonsterStateManager instance;
    private Dictionary<Enemy, MonsterState> monsterStates;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        monsterStates = new Dictionary<Enemy, MonsterState>();
    }

    public static MonsterStateManager Instance { get { return instance; } }

    public void AddMonster(Enemy enemy, MonsterState initialState)
    {
        monsterStates.Add(enemy, initialState);
    }

    public void ChangeState(Enemy enemy, MonsterState newState)
    {
        monsterStates[enemy].Exit();
        monsterStates[enemy] = newState;
        newState.Enter();
    }

    private void Update()
    {
        var enemyKeys = new List<Enemy>(monsterStates.Keys);

        foreach (Enemy enemy in monsterStates.Keys.ToList())
        {
            if (enemy == null || enemy.gameObject == null)
            {
                monsterStates.Remove(enemy);
                continue;
            }

            monsterStates[enemy].Update(Time.deltaTime);
        }
    }
}
