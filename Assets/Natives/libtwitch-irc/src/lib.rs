#![allow(unused)]

use std::ffi::c_void;
use std::sync::mpsc;
use std::thread::JoinHandle;

use twitch_irc::{
    login::StaticLoginCredentials as LoginCreds, ClientConfig as Config,
    SecureTCPTransport as TCPTransport, TwitchIRCClient as IRC,
};

mod twitch;

#[repr(C)]
struct Context {
    thread_handle: JoinHandle<()>,
    client: mpsc::Sender<String>,
}

#[no_mangle]
pub extern "C" fn init_runtime() -> *const c_void {
    let (mut trans, mut receiver) = std::sync::mpsc::channel::<String>();

    let handle = std::thread::spawn(move || {
        let r = tokio::runtime::Runtime::new().unwrap();    

        r.block_on(async move {
            let cfg = twitch_irc::ClientConfig::default();
            let (mut incoming, client) = IRC::<TCPTransport, LoginCreds>::new(cfg);

            let twitch_recv_handle = tokio::spawn(async move {
                while let Some(msg) = incoming.recv().await {
                    println!("Recieved: {:#?}", msg);
                }
            });

            while let Ok(ch) = receiver.recv() {
                match client.join(ch) {
                    Ok(_) => {},
                    Err(e) => eprintln!("Wrong format! E: {e}"),
                }
            }

            // Should be last since this blocks the thread
            twitch_recv_handle.await.unwrap();
        });
    });

    let ctx = Context {
        thread_handle: handle,
        client: trans
    };

    let handle = Box::new(ctx);
    Box::into_raw(handle).cast()
}

#[no_mangle]
pub extern "C" fn free_handle(handle: *mut c_void) {
    unsafe {
        let mut handle: *mut Context = handle.cast();



        drop(Box::from_raw(handle));
    }
}

#[no_mangle]
pub extern "C" fn join_channel(ctx: *mut c_void, s_ptr: *const u16, s_len: i32) {
    let name = unsafe { std::slice::from_raw_parts(s_ptr, s_len as usize) };
    let name = String::from_utf16(name).unwrap();
    let ctx: Box<Context> = unsafe {

        let ctx: *mut Context = ctx.cast();

        Box::from_raw(ctx)
    };

    ctx.client.send(name).unwrap();

}
