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

        _controller.AddSequence(new ToolTipSequence(_controller, tt, "test"))
            .AddSequence(new WaitSequence(_controller, 2f))
            .AddSequence(new ToolTipSequence(_controller, tt, "huh"))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-145, 60);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "fff"));

        // _controller.ManualStart();
    }

    public void StartSequences()
    {
        _controller.gameObject.SetActive(true);
        
        _controller.ManualStart();
    }
}
