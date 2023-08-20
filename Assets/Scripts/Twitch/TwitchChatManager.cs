using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwitchChatManager : MonoBehaviour
{
    private Twitch client;

    [SerializeField] private List<UnityEvent> outcomes;

    [SerializeField]
    private ulong[] counters;

    [SerializeField]
    [Min(0)]
    private float timeLimit;

    [SerializeField] private Image bar;
    [SerializeField] private Image box;

    private Coroutine routine;

    void Start()
    {
        var channel = PlayerPrefs.GetString("twitch_channel");
        if (channel == "")
        {
            Destroy(gameObject);
            return;
        }
        
        client = new Twitch();
        client.OnChat = OnChat;

        JoinChannel(channel);
    }

    private void OnDisable()
    {
        if (client == null) return;
        client.ExplicitlyDestroy();
    }
    
    private void OnChat(string msg)
    {
        try
        {
            var num = Convert.ToInt32(msg);
            if (num <= 0 || num > counters.Length) return;

            counters[num - 1]++;
        }
        catch { }
    }

    public void JoinChannel(string name)
    {
        client.JoinChannel(name);
        if (routine != null) return;
        routine = StartCoroutine(Timer(timeLimit));
    }

    private void Reset()
    {
        for (var i = 0; i < counters.Length; i++)
        {
            counters[i] = 0;
        }
    }

    private IEnumerator Timer(float limit)
    {
        StartCoroutine(ProgressBar());
        yield return new WaitForSeconds(limit);

        var max = counters.Max();
        
        if (max == 0) goto skip;

        var index = Array.IndexOf(counters, max);

        if (index > outcomes.Count)
        {
            Debug.LogError("Lengths are mismatched");
            yield break;
        }
        
        outcomes[index]?.Invoke();
        
        skip:
        
        Reset();
        Rest(limit);
    }

    private async void Rest(float limit)
    {
        box.gameObject.SetActive(false);
        await new WaitForSeconds(5);
        box.gameObject.SetActive(true);
        StartCoroutine(Timer(limit));
    }

    private IEnumerator ProgressBar()
    {
        var cur = 0f;
        for (;;)
        {
            yield return new WaitForEndOfFrame();

            if (Mathf.Approximately(bar.fillAmount, timeLimit))
            {
                yield break;
            }

            bar.fillAmount = cur / timeLimit;

            cur += Time.deltaTime;
        }
    }

    public void DebugFunction1()
    {
        Debug.Log("Received! 1");
    }
    
    public void DebugFunction2()
    {
        Debug.Log("Received! 2");
    }
    
    public void DebugFunction3()
    {
        Debug.Log("Received! 3");
    }
}
