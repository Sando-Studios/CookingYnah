using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public enum TargetStat
{
    VitStat,
    AgiStat,
    StrStat,
    VigStat,
    IntStat,
    EndStat,
    DexStat,
}

[CreateAssetMenu(fileName = "NewDropItem", menuName = "Item/DropObject")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private SerializedDictionary<TargetStat, int> PermaBuffDictionary = new SerializedDictionary<TargetStat, int>();
    [SerializeField] private float tempBuffDuration;
    [SerializeField] private SerializedDictionary<TargetStat, int> TempBuffsDictionary= new SerializedDictionary<TargetStat, int>();

    public string Name
    {
        get { return itemName; }
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

}
