using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class LevelMaskHandler : MonoBehaviour
{

    public GameObject maskObject;
    public GameObject mask;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            foreach (Material m in maskObject.GetComponent<MeshRenderer>().materials)
            {
                if (m.name != "Brick Floor Material (Instance)")
                    m.renderQueue = 3002;
            }
            mask.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == maskObject)
        {
            foreach (Material m in maskObject.GetComponent<MeshRenderer>().materials)
            {
                if (m.name != "Brick Floor Material (Instance)")
                    m.renderQueue = 2000;
            }
            mask.SetActive(false);
        }
    }
}
