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

        var originalPos = tt.GetComponent<RectTransform>().anchoredPosition;

        _controller.AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.gameObject.SetActive(true);
                Debug.Log("test");
                sequence.SetStatus(true);
            }))
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
                UIManager.instance.craftingBtnBlock = false;
                sequence.SetStatus(true);

                tt.gameObject.SetActive(false);
                tt.GetComponent<RectTransform>().anchoredPosition = originalPos;
                
                Reset();
            }));
    }

    public void Reset()
    {
        _controller.Clear();
        Setup();
    }

    public void StartSequences()
    {
        _controller.gameObject.SetActive(true);
        
        _controller.ManualStart();
    }
}
