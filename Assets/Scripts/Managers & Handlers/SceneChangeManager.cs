using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager instance;
    [SerializeField] private GameObject objectToLoad;
    [SerializeField] private GameObject playerSpawnPoint;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint");

        if (objectToLoad != null)
        {
            objectToLoad.transform.position = playerSpawnPoint.transform.position;
        }
    }

    public GameObject GetObjectToLoad()
    {
        return objectToLoad;
    }
}

