using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatDescription", menuName = "DataAsset/Stat Description")]
public class StatDescriptions : ScriptableObject
{
    [Header("VIT stat")]
    [TextArea]
    [SerializeField] private string vitDescription;

    [Header("AGI stat")]
    [TextArea]
    [SerializeField] private string agiDescription;

    [Header("STR stat")]
    [TextArea]
    [SerializeField] private string strDescription;

    [Header("VIG stat")]
    [TextArea]
    [SerializeField] private string vigDescription;

    [Header("INT stat")]
    [TextArea]
    [SerializeField] private string intDescription;

    [Header("END stat")]
    [TextArea]
    [SerializeField] private string endDescription;

    [Header("DEX stat")]
    [TextArea]
    [SerializeField] private string dexDescription;

    public string Vitality { get { return vitDescription; } }
    public string Agility { get { return agiDescription; } }
    public string Strength { get { return strDescription; } }
    public string Vigor { get { return vigDescription; } }
    public string Intelligence { get { return intDescription; } }
    public string Endurance { get { return endDescription; } }
    public string Dexterity { get { return dexDescription; } }
}
