use std::ffi::c_char;
use std::ffi::CString;
use std::sync::mpsc::Receiver;
use tokio::runtime;
use twitch_irc::{
    login::StaticLoginCredentials as LoginCreds, message::ServerMessage::Privmsg,
    ClientConfig as Config, SecureTCPTransport as TCPTransport, TwitchIRCClient as IRC,
};

pub fn fake_main(
    receiver: Receiver<String>,
    identifier: i32,
    callback: extern "C" fn(*mut c_char, i32) -> (),
) {
    let r = runtime::Runtime::new().unwrap();

    r.block_on(async move {
        let cfg = Config::default();
        let (mut incoming, client) = IRC::<TCPTransport, LoginCreds>::new(cfg);

        let twitch_recv_handle = tokio::spawn(async move {
            while let Some(msg) = incoming.recv().await {
                let Privmsg(msg) = msg else {
                    continue;
                };

                let message = CString::new(msg.message_text.as_str()).unwrap_or_default();

                callback(message.into_raw(), identifier);
            }
        });

        while let Ok(ch) = receiver.recv() {
            match client.join(ch) {
                Ok(_) => {}
                Err(e) => eprintln!("Wrong format! E: {e}"),
            }
        }

        drop(client);
        twitch_recv_handle.await.unwrap();
    });
}
