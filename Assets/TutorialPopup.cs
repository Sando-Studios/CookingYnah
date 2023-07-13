using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

using Text = TMPro.TextMeshProUGUI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private Text text;
    
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = camera.transform.position - transform.position;
        dir = dir.normalized * -1;

        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void ChangeText(string text)
    {
        this.text.text = text;
    }
}
