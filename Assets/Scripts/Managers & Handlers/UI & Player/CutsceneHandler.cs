using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<Sprite, float> scenesTimeDictionary = new SerializedDictionary<Sprite, float>();
    [SerializeField] private Image image;
    [SerializeField] private string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    private async void Play()
    {
        foreach (KeyValuePair<Sprite, float> pair in scenesTimeDictionary)
        {
            image.sprite = pair.Key;
            await new WaitForSeconds(pair.Value);
        }

        GoToNextScene();
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
