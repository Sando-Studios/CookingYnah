use std::ffi::CString;
use std::ffi::{c_char, c_void};
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
pub extern "C" fn init_runtime(identifier: i32, callback: Option<extern "C" fn(*mut c_char, i32) -> ()>) -> *mut c_void {
    if callback.is_none() {
        return std::ptr::null_mut();
    }

    let (sender, receiver) = mpsc::channel::<String>();

    let handle = thread::spawn(move || twitch::fake_main(receiver, identifier, callback.unwrap()));

    let ctx = Context {
        thread_handle: handle,
        client: sender,
    };

    let handle = Box::new(ctx);
    handle.into()
}

#[no_mangle]
pub extern "C-unwind" fn free_handle(handle: *mut c_void) {
    let ctx = unsafe {
        let handle: *mut Context = handle.cast();

        Box::from_raw(handle)
    };

    drop(ctx.client);

    match ctx.thread_handle.join() {
        Ok(_) => {}
        Err(_) => eprintln!("Unexpected error in thread."),
    }
}

/// Joins a twitch channel with the provided name
/// # Safety
/// - Please use the provided binding for the C# file.
/// - If you want a custom implementation, `s_ptr` is the location of the string ptr, while s_len will be the length.
/// - Take note that this does not include the null character at the end.
/// - Don't forget to put the void* reference first
/// - The passed string must be UTF-16 encoded, if not, there might be undefined behavior
/// # Examples
/// C# code to bind with the function
/// ```ignore
/// var str = "koolieaid";
///  fixed (char* p = str)
///  {
///      RawTwitch.join_channel(runtime, (ushort*)p, str.Length);
///  }
/// ```
#[no_mangle]
pub unsafe extern "C-unwind" fn join_channel(ctx: *mut c_void, s_ptr: *const u16, s_len: i32) {
    use std::slice;

    let name = unsafe { slice::from_raw_parts(s_ptr, s_len as usize) };
    let name = String::from_utf16(name).unwrap();
    let ctx: Box<Context> = ctx.into();

    ctx.client.send(name).unwrap();

    Box::into_raw(ctx);
}

/// Frees the string that was pass to the current program from the library
/// # Safety
/// **string** must be a pointer passed down by void (*fn)(const char*).
/// It must also be from Rust, not any other language
#[no_mangle]
pub unsafe extern "C-unwind" fn free_string(string: *mut c_char) {
    let cstr = unsafe {
        CString::from_raw(string)
    };
    drop(cstr);
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
