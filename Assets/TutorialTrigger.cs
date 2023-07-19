using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private CraftingTutorial tut;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out var player)) return;
        
        UIManager.instance.ForceOpenCraftingPanel();
        
        tut.StartSequences();
    }
}
