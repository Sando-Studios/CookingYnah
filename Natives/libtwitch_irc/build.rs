use std::fs::File;

const DLLNAME: &str = "libtwitch_irc.dll";
const PROJECT_SETTINGS_PATH: &str = "../../ProjectSettings/ProjectSettings.asset";

#[derive(Debug, serde::Deserialize)]
struct ProjectSettings {
    #[serde(rename(deserialize = "PlayerSettings"))]
    player_settings: PlayerSettings,
}

#[derive(Debug, serde::Deserialize)]
struct PlayerSettings {
    #[allow(dead_code)]
    #[serde(rename(deserialize = "companyName"))]
    company_name: String,
    #[serde(rename(deserialize = "productName"))]
    product_name: String,
}

impl ProjectSettings {
    pub fn get_product_name(&self) -> &str {
        &self.player_settings.product_name
    }
}

fn main() {
    let file = File::open(PROJECT_SETTINGS_PATH).expect("Cannot find project settings path.");
    
    let settings: ProjectSettings = serde_yaml::from_reader(file).expect("Unable to read project settings file");
    
    let editor_path = format!("Assets/Plugins/Twitch/{}", DLLNAME);
    let standalone_path = format!("{}_Data/Plugins/x86_64/{}", settings.get_product_name(), DLLNAME);

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
        
    if let Err(e) = res {
        eprint!("Error generating bindings: {e:?}");
    }
}
