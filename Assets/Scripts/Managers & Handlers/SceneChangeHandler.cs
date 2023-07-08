using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private bool canUse;

    public void SetCanUse(bool isUsable)
    {
        canUse = isUsable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger && canUse)
            SceneManager.LoadScene(sceneName);
    }
}
