using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class TwitchChatManager : MonoBehaviour, IChatListener
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

    [SerializeField] private Text[] votes;

    private Coroutine routine;

    [Header("Jokes")]
    [SerializeField] private GameObject eggPrefab;

    void Start()
    {
        var channel = PlayerPrefs.GetString("twitch_channel");
        if (channel == "")
        {
            Destroy(gameObject);
            return;
        }
        
        client = new Twitch(this);

        JoinChannel(channel);
    }
    
    private event Action callbackQueue;
    private event Action eventBuffer; // Prevents race conditions
 
    void Update()
    {
        if (callbackQueue != null)
        {
            eventBuffer = callbackQueue;
            callbackQueue = null;
            eventBuffer.Invoke();
            eventBuffer = null;
        }
    }

    private void OnDisable()
    {
        if (client == null) return;
        client.ExplicitlyDestroy();
    }
    
    public void OnChat(string msg)
    {
        try
        {
            var num = Convert.ToInt32(msg);
            if (num <= 0 || num > counters.Length) return;

            counters[num - 1]++;

            callbackQueue += () => votes[num - 1].text = $"{counters[num - 1].ToString()} votes";
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
            votes[i].text = "0 votes";
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

    public async void SpawnEgg()
    {
        var player = UIManager.instance.player;

        for (var i = 0; i < 5; i++)
        {
            var egg = Instantiate(eggPrefab, player.gameObject.transform.position + Vector3.up * 3, Quaternion.identity);
            
            egg.GetComponent<EggGrenade>().SetExplosionData(200, -1);
            await new WaitForSeconds(0.3f);
        }

    }
}
