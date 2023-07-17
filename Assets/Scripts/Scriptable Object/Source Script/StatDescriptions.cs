using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatDescription", menuName = "DataAsset/Stat Description")]
public class StatDescriptions : ScriptableObject
{
    [Header("VIT stat")]
    [TextArea]
    [SerializeField] private string vitDescription;

    [Header("STR stat")]
    [TextArea]
    [SerializeField] private string strDescription;

    [Header("RES stat")]
    [TextArea]
    [SerializeField] private string resDescription;

    public string Vitality { get { return vitDescription; } }
    public string Strength { get { return strDescription; } }
    public string Resilience { get { return resDescription; } }
}
