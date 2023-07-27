using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Asyncoroutine;

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
        if (scene.name == "Main Level")
        {
            mask.SetActive(false);
            return;
        }

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
            StartScaling(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == maskObject)
        {

            StartScaling(-1);
        }
    }

    public Vector3 maxScale = new Vector3(3.229519f, 1.794177f, 2.474171f);
    public Vector3 minScale = Vector3.zero;
    public float scalingDuration = 1.0f;

    private bool isScaling = false;
    private int scalingDirection = 0; // 1 for scaling up, -1 for scaling down
    private Vector3 initialScale; // Store the initial scale before scaling down

    public void StartScaling(int direction)
    {
        scalingDirection = direction;
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine());

    }

    private IEnumerator ScaleCoroutine()
    {
        isScaling = true;
        Vector3 targetScale = (scalingDirection.Equals(1)) ? maxScale : minScale;
        initialScale = mask.transform.localScale; // Store the initial scale

        float elapsedTime = 0f;

        while (elapsedTime < scalingDuration)
        {
            mask.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scalingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mask.transform.localScale = targetScale;
        if (scalingDirection.Equals(-1))
        {
            material.renderQueue = initialMaterialRenderQueue;
            mask.SetActive(false);
        }
        isScaling = false;
    }
}
