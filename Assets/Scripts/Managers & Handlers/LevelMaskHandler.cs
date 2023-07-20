using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaskHandler : MonoBehaviour
{
    public GameObject maskObject;
    public GameObject mask;
    private Material material;
    private int initialMaterialRenderQueue;

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
        if (maskObject == null)
            maskObject = GameObject.FindGameObjectWithTag("Floor");
        
        foreach (Material m in maskObject.GetComponent<MeshRenderer>().materials)
        {
            if (m.name.Contains("Top"))
            {
                material = m;
                initialMaterialRenderQueue = m.renderQueue;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            material.renderQueue = 4002;
            mask.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            material.renderQueue = initialMaterialRenderQueue;
            mask.SetActive(false);
        }
    }
}
