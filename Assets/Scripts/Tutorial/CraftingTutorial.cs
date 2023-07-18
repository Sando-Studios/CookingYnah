using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class CraftingTutorial : MonoBehaviour
{
    [SerializeField]
    private SequenceController _controller;

    [SerializeField] private RectTransform popUp;
    
    // Start is called before the first frame update
    void Start()
    {
        var tt = popUp.GetComponent<ToolTipAdapter>();

        _controller.AddSequence(new ToolTipSequence(_controller, tt, "test"));
        
        _controller.ManualStart();
    }

    private void StartSequences()
    {
        _controller.ManualStart();
    }
}
