using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TargetStat stat;


    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.OnCursorOverStat(stat);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.OnCursorOffStat();
    }
}
