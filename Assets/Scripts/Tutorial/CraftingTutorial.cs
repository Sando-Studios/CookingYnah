using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class CraftingTutorial : MonoBehaviour
{
    [SerializeField]
    private SequenceController _controller;

    [SerializeField] private RectTransform popUp;

    public void Setup()
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
            .AddSequence(new ToolTipSequence(_controller, tt, "fff"))
            .AddSequence(new WaitSequence(_controller, 3f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                UIManager.instance.player.EnableInputs();
                UIManager.instance.ForceCloseCraftingPanel();
                sequence.SetStatus(true);
            }));
    }

    public void StartSequences()
    {
        _controller.gameObject.SetActive(true);
        
        _controller.ManualStart();
    }
}