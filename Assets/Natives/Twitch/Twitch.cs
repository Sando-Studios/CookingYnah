using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using RawNative;

public interface IChatListener
{
    /// <summary>
    /// Method is called when a Twitch chat is received
    /// </summary>
    /// <remarks>
    /// If the implementor is a MonoBehavior, please add the following code:
    /// 
    /// <code>
    /// private event Action callbackQueue;
    /// private event Action eventBuffer; // Prevents race conditions
    /// 
    /// void Update()
    /// {
    ///     if (callbackQueue != null)
    ///     {
    ///         eventBuffer = callbackQueue;
    ///         callbackQueue = null;
    ///         eventBuffer.Invoke();
    ///         eventBuffer = null;
    ///     }
    /// }
    /// </code>
    ///
    /// and to add a callback do callbackQueue += () => Function();
    ///
    /// This is all because of Unity not wanting some methods not being executed on the main thread,
    /// hence we add a queue, and call them later on an Update frame from the main thread.
    /// </remarks>
    /// <param name="message"></param>
    void OnChat(string message);
}

public unsafe class Twitch
{
    private static Dictionary<int, IChatListener> clients = new();
    
    private void* runtime;
    
    public Twitch(IChatListener listener)
    {
        var hash = GetHashCode();
        
        if (clients.ContainsKey(hash)) return;
        
        runtime = RawTwitch.init_runtime(hash, RawListener);
        clients.Add(hash, listener);
    }

    ~Twitch()
    {
        if (runtime == null) return;

        ExplicitlyDestroy();
    }
    
    [MonoPInvokeCallback(typeof(RawTwitch.init_runtime_callback_delegate))]
    private static void RawListener(byte* str, int identifier)
    {
        var msg = new string((sbyte*)str);
        RawTwitch.free_string(str);

        try
        {
            clients[identifier]?.OnChat(msg);
        }
        catch (Exception e)
        {
            const string info = "if this is a main thread error, please consult the documentation of \"IChatListener\"";
            UnityEngine.Debug.LogError($"{e.Message}. \n {info}");
        }
    }

    public void JoinChannel(string channelName)
    {
        if (runtime == null)
        {
            throw new InvalidOperationException("Runtime has been destroyed");
        }
        
        var str = String.Copy(channelName);
        fixed (char* p = str)
        {
            RawTwitch.join_channel(runtime, (ushort*)p, str.Length);
        }
    }

    public void ExplicitlyDestroy()
    {
        if (runtime == null)
        {
            throw new InvalidOperationException("Runtime has already been destroyed");
        }
        RawTwitch.free_handle(runtime);
        runtime = null;

        clients.Remove(GetHashCode());
    }
}
