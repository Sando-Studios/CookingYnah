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

[CreateAssetMenu(fileName = "NewDropItem", menuName = "Item/Drop")]
public class DropItemData : ScriptableObject
{
    [SerializeField] private SerializedDictionary<TargetStat, int> DropBuffsDictionary = new SerializedDictionary<TargetStat, int>();

    public SerializedDictionary<TargetStat, int> DropBuffs
    {
        get { return DropBuffsDictionary; }
    }

}
