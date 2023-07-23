using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBuffUIIconHandler : MonoBehaviour
{
    public GameObject iconPrefab;
    

    private void OnEnable()
    {
        TempEffectHandler.OnNewTempBuff += SpawnNewIcon;
    }
    private void OnDisable()
    {
        TempEffectHandler.OnNewTempBuff -= SpawnNewIcon;
    }

    private void SpawnNewIcon(TargetStat stat, float duration)
    {
        GameObject clone = Instantiate(iconPrefab, transform);
        clone.GetComponent<TempBuffIcon>().SetData(stat, duration);
        clone.transform.SetAsLastSibling();
    }
}
