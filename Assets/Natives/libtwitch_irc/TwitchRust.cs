// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;

namespace RustFFI
{
    internal static unsafe partial class TwitchRustRaw
    {
        const string __DllName = "Assets/Natives/libtwitch_irc/target/release/libtwitch_irc.dll";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void init_runtime_callback_delegate(byte* @byte);

        [DllImport(__DllName, EntryPoint = "init_runtime", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* init_runtime(init_runtime_callback_delegate callback);

        [DllImport(__DllName, EntryPoint = "free_handle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void free_handle(void* handle);

        [DllImport(__DllName, EntryPoint = "join_channel", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void join_channel(void* ctx, ushort* s_ptr, int s_len);

        [DllImport(__DllName, EntryPoint = "free_string", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void free_string(byte* @string);


    }



}
    