using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CinemachineVirtualCamera cineCamera;
    
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

    public void SetTarget()
    {
        GameObject g = SceneChangeManager.instance.GetObjectToLoad();
        cineCamera.Follow = g.transform;
        cineCamera.LookAt = g.transform;
    }

}
