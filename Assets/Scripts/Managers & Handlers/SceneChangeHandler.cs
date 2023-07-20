using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private bool canUse;
    [SerializeField] private GameObject blockObject;

    private void Start()
    {
        SetCanUse(canUse);
    }
    public void SetCanUse(bool isUsable)
    {
        canUse = isUsable;
        if (blockObject)
            blockObject.SetActive(!canUse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && canUse)
            SceneManager.LoadScene(sceneName);
    }
}
