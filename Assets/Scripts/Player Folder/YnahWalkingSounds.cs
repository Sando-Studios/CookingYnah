using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YnahWalkingSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Ground Specific Audio")]
    [SerializeField] private AudioClip rockAudio;
    [SerializeField] private AudioClip concreteAudio;
    [SerializeField] private AudioClip groundAudio;
    [SerializeField] private AudioClip carpetAudio;

    private string currentScene;
    private bool isWalking;

    private int numOfRugsOn = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        SetFloorAudio();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rug"))
        {
            numOfRugsOn++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rug"))
        {
            numOfRugsOn--;
        }
    }

    private void SetFloorAudio()
    {
        if (currentScene.Contains("Main"))
            audioSource.clip = groundAudio;
        else if (currentScene.Contains("1"))
            audioSource.clip = rockAudio;
        else if (currentScene.Contains("2") || currentScene.Contains("3"))
            audioSource.clip = concreteAudio;
    }

    public void OnPlayerMove(bool status)
    {
        isWalking = status;

        if (isWalking && !audioSource.isPlaying)
            audioSource.Play();
        else if (!isWalking && audioSource.isPlaying)
            audioSource.Stop();
    }
    private void Update()
    {
        if (numOfRugsOn > 0 && audioSource.clip != carpetAudio) audioSource.clip = carpetAudio;
        else if (numOfRugsOn == 0 && audioSource.clip == carpetAudio) SetFloorAudio();
    }
}
