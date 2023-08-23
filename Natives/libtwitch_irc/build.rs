use std::{fs::File, ops::Deref, error::Error};
use serde::Deserialize;

const DLLNAME: &str = "libtwitch_irc.dll";
const PROJECT_SETTINGS_PATH: &str = "../../ProjectSettings/ProjectSettings.asset";

#[derive(Debug, Deserialize)]
struct ProjectSettings {
    #[serde(rename(deserialize = "PlayerSettings"))]
    player_settings: PlayerSettings,
}

#[derive(Debug, Deserialize)]
struct PlayerSettings {
    #[serde(rename(deserialize = "companyName"))]
    company_name: String,
    #[serde(rename(deserialize = "productName"))]
    product_name: String,
}

impl Deref for ProjectSettings {
    type Target = PlayerSettings;
    
    fn deref(&self) -> &Self::Target {
        &self.player_settings
    }
}

impl PlayerSettings {
    pub fn get_product_name(&self) -> &str {
        &self.product_name
    }
    
    #[allow(dead_code)]
    pub fn get_company_name(&self) -> &str {
        &self.company_name
    }
}

fn main() -> Result<(), Box<dyn Error>> {
    let file = File::open(PROJECT_SETTINGS_PATH)?;
    
    let settings: ProjectSettings = serde_yaml::from_reader(&file)?;
    
    let editor_path = format!("Assets/Plugins/Twitch/{}", DLLNAME);
    let standalone_path = format!("{}_Data/Plugins/x86_64/{}", settings.get_product_name(), DLLNAME);

    csbindgen::Builder::default()
        .input_extern_file("src/lib.rs")
        .csharp_dll_name(&editor_path)
        .csharp_namespace("RawNative")
        .csharp_class_name("RawTwitch")
        .csharp_use_function_pointer(false)
        .csharp_dll_name_if("!UNITY_EDITOR", &standalone_path)
        .generate_to_file(
            "../../Assets/Plugins/libtwitch_irc.dll", // This is probably ignored, idk the inner workings of the build lib
            "../../Assets/Natives/Twitch/TwitchRust.cs",
        )?;
    
    Ok(())
}
