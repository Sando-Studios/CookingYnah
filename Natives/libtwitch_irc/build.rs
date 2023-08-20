const DLLNAME: &str = "libtwitch_irc.dll";

fn main() {
    let name = std::env::var("GAME_NAME").ok();
    
    let editor_path = format!("Assets/Plugins/Twitch/{}", DLLNAME);
    let standalone_path = format!("{}_Data/Plugins/x86_64/{}", name.unwrap_or_default(), DLLNAME);

    let res = csbindgen::Builder::default()
        .input_extern_file("src/lib.rs")
        .csharp_dll_name(&editor_path)
        .csharp_namespace("RawNative")
        .csharp_class_name("RawTwitch")
        .csharp_use_function_pointer(false)
        .csharp_dll_name_if("!UNITY_EDITOR", &standalone_path)
        .generate_to_file(
            "../../Assets/Plugins/libtwitch_irc.dll", // This is probably ignored, idk the inner workings of the build lib
            "../../Assets/Natives/Twitch/TwitchRust.cs",
        );

    match res {
        Ok(_) => {}
        Err(e) => eprintln!("Error building: {e}"),
    }
}
