using System;
using UnityEngine;

public class WaypointsHandler : MonoBehaviour
{
    public static Action<int, GameObject[]> OnGetWaypoints;

    [SerializeField] private GameObject[] waypoints;

    private void OnEnable()
    {
        DuckEnemy.OnDuckSpawn += GetWaypoints;
    }
    private void OnDisable()
    {
        DuckEnemy.OnDuckSpawn -= GetWaypoints;
    }

    private void GetWaypoints(int id)
    {
        OnGetWaypoints?.Invoke(id, waypoints);
    }
    


}
