using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public enum TargetStat
{
    VitStat,
    StrStat,
    ResStat,
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [TextArea]
    [SerializeField] private string itemDescription;

    [Header("Item Data")]
    [SerializeField] private SerializedDictionary<TargetStat, int> PermaBuffDictionary = new SerializedDictionary<TargetStat, int>();
    [Tooltip("In Seconds")]
    [SerializeField] private float tempBuffDuration;
    [SerializeField] private SerializedDictionary<TargetStat, int> TempBuffsDictionary= new SerializedDictionary<TargetStat, int>();

    [SerializeField] private int healAmount;

    public string Name
    {
        get { return itemName; }
    }
    public string Description
    {
        get { return itemDescription; }
    }
    public Sprite SpriteToRender
    {
        get { return itemSprite; }
    }
    public float Duration
    {
        get { return tempBuffDuration; }
    }

    public SerializedDictionary<TargetStat, int> TemporaryBuffs
    {
        get { return TempBuffsDictionary; }
    }

    public SerializedDictionary<TargetStat, int> PermanentBuffs
    {
        get { return PermaBuffDictionary; }
    }

    public int HealAmount
    {
        get { return healAmount; }
    }
}
