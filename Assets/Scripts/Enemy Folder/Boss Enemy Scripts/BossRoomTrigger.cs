using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private MajorEnemy boss;
    [SerializeField] private AudioSource audioSource;

    public static Action<int> OnEnterBossRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            boss.SetPlayerStatus(true, other.gameObject);
            audioSource.Play();
            OnEnterBossRoom?.Invoke(1);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            boss.SetPlayerStatus(false, null);
            audioSource.Stop();
            OnEnterBossRoom?.Invoke(-1);
        }

    }
}
