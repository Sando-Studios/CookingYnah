using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Yea im not proud of it, i got no time to worry about speed
        var jab = GameObject.Find("Jab").GetComponent<Jab>();
        jab.Start();
    }
}
