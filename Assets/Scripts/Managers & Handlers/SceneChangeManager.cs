using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToLoad;

    [SerializeField] private GameObject playerSpawnPoint;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(objectToLoad);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint");
        
        Player.Instance.transform.position = playerSpawnPoint.transform.position;
    }
}
