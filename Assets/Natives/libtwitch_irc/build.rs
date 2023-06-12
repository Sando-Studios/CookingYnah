fn main() {
    let res = csbindgen::Builder::default()
        .input_extern_file("src/lib.rs")
        .csharp_dll_name("Assets/Natives/libtwitch_irc/target/release/libtwitch_irc.dll")
        .csharp_namespace("RustFFI")
        .csharp_class_name("TwitchRustRaw")
        .csharp_use_function_pointer(false)
//        .generate_csharp_file("TwitchRust.cs");
        .generate_to_file("../../Plugins/libtwitch_irc.dll", "TwitchRust.cs").unwrap();

//    match res {
//        Ok(_) => {}
//        Err(e) => println!("error thing: {}", e),
//    }
}
