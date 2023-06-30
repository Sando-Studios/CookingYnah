using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaskHandler : MonoBehaviour
{
    public GameObject maskObject;
    public GameObject mask;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Material m in maskObject.GetComponent<MeshRenderer>().materials)
        {
            if (m.name.Contains("Gray 2"))
                m.renderQueue = 3002;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            mask.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            mask.SetActive(false);
        }
    }
}
