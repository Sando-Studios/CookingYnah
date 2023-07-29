using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMHandler : MonoBehaviour
{
    public static BGMHandler instance;
    public AudioSource mainMenuTheme;
    public AudioSource mainLevelTheme;
    public AudioSource d1Theme;
    public AudioSource d2Theme;
    public AudioSource d3Theme;

    private string currentSceneName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        BossRoomTrigger.OnEnterBossRoom += PlayerEnterBossRoom;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        BossRoomTrigger.OnEnterBossRoom -= PlayerEnterBossRoom;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        UpdateMusic();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateMusic()
    {
        switch (currentSceneName)
        {
            case "Main Level":
                PauseAllAudio();
                mainLevelTheme.Play();
                break;
            case "Dungeon 1 Level":
                PauseAllAudio();
                d1Theme.Play();
                break;
            case "Dungeon 2 Level":
                PauseAllAudio();
                d2Theme.Play();
                break;
            case "Dungeon 3 Level":
                PauseAllAudio();
                d3Theme.Play();
                break;
            case "MainMenu":
                PauseAllAudio();
                mainMenuTheme.Play();
                break;
            case "Ending Cutscene":
                PauseAllAudio();
                mainMenuTheme.Play();
                break;
        }
    }

    public void PauseAllAudio()
    {
        d1Theme.Pause();
        d2Theme.Pause();
        d3Theme.Pause();
        mainMenuTheme.Pause();
        mainLevelTheme.Pause();
    }

    private void PlayerEnterBossRoom(int i)
    {
        switch (i)
        {
            case 1:
                PauseAllAudio();
                break;
            case -1:
                UpdateMusic();
                break;
        }
    }
}
