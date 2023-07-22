using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Tooltip("This script will play a random Audio Clip from the Array for every OnTriggerEnter() with a Enemy or Player")]
    [SerializeField] private AudioClip[] clips;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
