use std::ffi::c_void;
use std::sync::mpsc;
use std::thread::JoinHandle;

mod twitch;

#[repr(C)]
struct Context {
    thread_handle: JoinHandle<()>,
    transmitter: mpsc::Sender<String>,
}

pub extern "C" fn init_runtime() -> *const c_void {
    let handle = std::thread::spawn(|| {
        let r = tokio::runtime::Runtime::new().unwrap();    

        r.block_on(twitch::boot());
    });

    let handle = Box::new(handle);
    Box::into_raw(handle).cast()
}

pub extern "C" fn free_handle(handle: *const c_void) {
    unsafe {
        let mut handle: *mut Context = std::mem::transmute(handle);

        drop(Box::from_raw(handle));
    }
}

pub extern "C" fn join_channel(ctx: *mut c_void, s_ptr: *const u8, s_len: i32) {
    let ctx: Box<Context> = unsafe {

        let ctx: *mut Context = ctx.cast();

        Box::from_raw(ctx)
    };

    ctx.transmitter.send("koolieaid".into()).unwrap();

}
