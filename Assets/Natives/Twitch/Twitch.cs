using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using RawNative;

public interface IChatListener
{
    void OnChat(string message);
}

public unsafe class Twitch
{
    private static Dictionary<int, IChatListener> clients = new();
    
    private void* runtime;
    
    public Twitch(IChatListener listener)
    {
        var hash = listener.GetHashCode();
        
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
        
        clients?[identifier]?.OnChat(msg);
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
