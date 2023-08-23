using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TwitchDebugger : MonoBehaviour, IChatListener
{
    private Twitch client;

    private Dictionary<string, Action<string>> outcomes = new();

    public Debugger dbg;
    
    private event Action callbackQueue;
    private event Action eventsClone; // Prevents race conditions
 
    void Update()
    {
        if (callbackQueue != null)
        {
            eventsClone = callbackQueue;
            callbackQueue = null;
            eventsClone.Invoke();
            eventsClone = null;
        }
    }
    
    private void Start()
    {
        outcomes.Add("say", Say);
        outcomes.Add("max", Over9000);
        outcomes.Add("tpboss", TpBoss);
        outcomes.Add("unlockall", UnlockAll);
        
        client = new Twitch(this);
        
        var channel = PlayerPrefs.GetString("twitch_channel");
        if (channel == "") return;
        client.JoinChannel(channel);
    }

    private void OnDisable()
    {
        client.ExplicitlyDestroy();
    }
    
    public void OnChat(string msg)
    {
        var args = msg.Split(" ", 2);
        
        if (!outcomes.ContainsKey(args[0])) return;

        callbackQueue += () => outcomes[args[0]]?.Invoke(args.Length > 1 ? args[1] : null);
    }

    public void Say([CanBeNull] string arg)
    {
        if (arg == null) arg = "nothing";
        
        Debug.Log($"chat said {arg}");
    }

    public void UnlockAll(string arg)
    {
        dbg.UnlockArtifacts();
    }

    public void Over9000(string arg)
    {
        dbg.Max();
    }

    public void TpBoss(string arg)
    {
        dbg.TeleportToChamber();
    }
    
}
