use std::sync::mpsc::Receiver;
use tokio::runtime;
use twitch_irc::{
    login::StaticLoginCredentials as LoginCreds, ClientConfig as Config,
    SecureTCPTransport as TCPTransport, TwitchIRCClient as IRC,
};

pub fn fake_main(receiver: Receiver<String>) {
    let r = runtime::Runtime::new().unwrap();

    r.block_on(async move {
        let cfg = Config::default();
        let (mut incoming, client) = IRC::<TCPTransport, LoginCreds>::new(cfg);

        let twitch_recv_handle = tokio::spawn(async move {
            while let Some(msg) = incoming.recv().await {
                println!("Recieved: {:#?}", msg);
            }
        });

        while let Ok(ch) = receiver.recv() {
            match client.join(ch) {
                Ok(_) => {}
                Err(e) => eprintln!("Wrong format! E: {e}"),
            }
        }

        // Should be last since this blocks the thread
//        twitch_recv_handle.await.unwrap();
    });
}
