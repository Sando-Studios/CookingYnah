using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using UnityEngine;

public class TwitchDebugger : MonoBehaviour
{
    private Twitch client;

    [SerializeField] [SerializedDictionary("Command", "Action / Event")]
    private SerializedDictionary<string, Action<string>> outcomes;
    
    private void Start()
    {
        outcomes.Add("say", Say);
        outcomes.Add("over9k", Over9000);
        
        client = new Twitch();
        client.OnChat = OnChat;
        client.JoinChannel("koolieaid");
    }

    private void OnDisable()
    {
        client.ExplicitlyDestroy();
    }
    
    private void OnChat(string msg)
    {
        var args = msg.Split(" ", 2);
        
        if (!outcomes.ContainsKey(args[0])) return;

        if (args.Length > 1)
        {
            outcomes[args[0]]?.Invoke(args[1]);
            return;
        }
        
        outcomes[args[0]]?.Invoke(null);
    }

    public void Say([CanBeNull] string arg)
    {
        if (arg == null) arg = "nothing";
        
        Debug.Log($"chat said {arg}");
    }

    public void Over9000(string arg)
    {
        Debug.Log("Vitality changed to 9k");
        UIManager.instance.player.GetPlayerData().Vitality = 9000;
    }
    
}
