using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]private MajorEnemy boss;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
            boss.SetPlayerStatus(true, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
            boss.SetPlayerStatus(false, null);
    }
}
