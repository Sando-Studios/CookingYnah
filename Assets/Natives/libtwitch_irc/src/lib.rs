#![allow(unused)]

use std::ffi::c_void;
use std::panic;
use std::sync::mpsc;
use std::thread;
use std::thread::JoinHandle;
mod twitch;

#[repr(C)]
struct Context {
    thread_handle: JoinHandle<()>,
    client: mpsc::Sender<String>,
}

#[no_mangle]
pub extern "C" fn init_runtime() -> *mut c_void {
    let (trans, receiver) = mpsc::channel::<String>();

    let handle = thread::spawn(move || twitch::fake_main(receiver));

    let ctx = Context {
        thread_handle: handle,
        client: trans,
    };

    let handle = Box::new(ctx);
    handle.into()
}

#[no_mangle]
pub extern "C" fn free_handle(handle: *mut c_void) {
    unsafe {
        let handle: *mut Context = handle.cast();

        let ctx = Box::from_raw(handle);

        //        ctx.client.send(twitch::SHUTDOWN_KEY.into());
        drop(ctx.client);

        println!("before main join");
        ctx.thread_handle.join();
        println!("after main join");
        //        drop(ctx);
    }
}

#[no_mangle]
pub extern "C" fn join_channel(ctx: *mut c_void, s_ptr: *const u16, s_len: i32) {
    use std::slice;

    let name = unsafe { slice::from_raw_parts(s_ptr, s_len as usize) };
    let name = String::from_utf16(name).unwrap();
    let ctx: Box<Context> = ctx.into();

    ctx.client.send(name).unwrap();
}

#[no_mangle]
pub extern "C" fn register_chat_callback(
    ctx: *mut c_void,
    callback: Option<extern "C" fn() -> ()>,
) {
    let mut ctx = Box::<Context>::from(ctx);

    let Some(callback) = callback else {
        return;
    };

    twitch::switch_listener(callback);
}

#[no_mangle]
pub extern "C" fn switch_listener_raw(callback: Option<extern "C" fn() -> ()>) {
    let Some(callback) = callback else {
        return;
    };

    twitch::switch_listener(callback);
}

impl From<*mut c_void> for Box<Context> {
    fn from(value: *mut c_void) -> Self {
        unsafe {
            let ctx: *mut Context = value.cast();
            Box::from_raw(ctx)
        }
    }
}

impl From<Box<Context>> for *mut c_void {
    fn from(value: Box<Context>) -> Self {
        Box::into_raw(value).cast()
    }
}
