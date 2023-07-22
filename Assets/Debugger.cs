using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;

using InputField = TMPro.TMP_InputField;

public class Debugger : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, UnityEvent> actions;

    [SerializeField] private InputField field;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            field.gameObject.SetActive(true);
        }
        
        if (Input.GetKey(KeyCode.Return))
        {
            DoTheDougie(field.text);
            field.text = "";
            field.gameObject.SetActive(false);
        }
    }

    private void DoTheDougie(string arg)
    {
        if (!actions.ContainsKey(arg)) return;

        actions[arg].Invoke();
    }

    public void Testshit()
    {
        Debug.Log("sex");
    }
}
