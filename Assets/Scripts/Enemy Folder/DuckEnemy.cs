using System;
using UnityEngine;
using UnityEngine.AI;


public class DuckEnemy : Enemy
{
    public static Action<int> OnDuckSpawn;
    private GameObject[] waypoints;

    protected override void OnEnable()
    {
        base.OnEnable();
        WaypointsHandler.OnGetWaypoints += SetWaypoints;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        WaypointsHandler.OnGetWaypoints -= SetWaypoints;
    }

    private void SetWaypoints(int id, GameObject[] waypointArray)
    {
        if (id == enemyDataInstance.UnitID)
            waypoints = waypointArray;
    }

    public override void SetEnemyData(int enemyID, EnemyUnitData unitData, Vector3 homeBase)
    {
        
        base.SetEnemyData(enemyID, unitData, homeBase);
        OnDuckSpawn?.Invoke(enemyID);
    }

    public override bool IsAlive()
    {
        return base.IsAlive();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void ResetAggro()
    {
        base.ResetAggro();
    }

    public override Vector3 GetHome()
    {
        return base.GetHome();
    }
    public override Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = waypoints[(int)UnityEngine.Random.Range(0f, waypoints.Length)].transform.position;
        NavMesh.SamplePosition(randomPoint, out NavMeshHit point, 7.0f, NavMesh.AllAreas);
        return point.position;
    }
}
