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
                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "Here is the list of recipes."))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-563, -126);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "This will be your guide in cooking meals"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-622, 157);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "This is your inventory panel"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(770, -1);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "To create meals, use this panel"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(45, 294);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "Place your protein in the first column of boxes"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(206, 297);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "Place your carbs in the second column of boxes"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(336, 298);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "Place your veggies on the last column"))
            .AddSequence(new WaitSequence(_controller, 4f))
            .AddSequence(new CustomSequence(_controller, (sequence, o) =>
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(138, -235);

                sequence.SetStatus(true);
            }))
            .AddSequence(new ToolTipSequence(_controller, tt, "Then click the cook button to cook your meal!"))
            .AddSequence(new WaitSequence(_controller, 4f))
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
