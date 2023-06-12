use std::sync::mpsc::Receiver;
use tokio::runtime;
use twitch_irc::{
    login::StaticLoginCredentials as LoginCreds,
    message::ServerMessage::{self, Privmsg},
    ClientConfig as Config, SecureTCPTransport as TCPTransport, TwitchIRCClient as IRC,
};
use std::sync::Arc;

pub fn fake_main(receiver: Receiver<String>, arc_callback: Option<extern "C" fn() -> ()>) {
    let r = runtime::Runtime::new().unwrap();

    println!("fake main");

    r.block_on(async move {
        println!("block time");

        let cfg = Config::default();
        let (mut incoming, client) = IRC::<TCPTransport, LoginCreds>::new(cfg);

        println!("make handle");

        let twitch_recv_handle = tokio::spawn(async move {

            while let Some(msg) = incoming.recv().await {

                let Privmsg(msg) = msg else {
                    continue;
                };

                println!("new msssg: {}", msg.message_text);

                match arc_callback.as_ref() {
                    Some(cb) => cb(),
                    None => {},
                }

            }
        });

        println!("done make hand");

        //        std::thread::sleep(std::time::Duration::from_secs(3));
        client.join("koolieaid".into()).unwrap();

        println!("start recv loop");
        // I have no clue how to stop this LMAO
        while let Ok(ch) = receiver.recv() {
            match client.join(ch) {
                Ok(_) => {}
                Err(e) => eprintln!("Wrong format! E: {e}"),
            }
        }

        drop(client);
        twitch_recv_handle.await.unwrap();

        println!("end loop");
    });

    println!("outside block");
}

unsafe extern "C" fn empty_listener() {}

static mut CB: unsafe extern "C" fn() -> () = empty_listener;

pub fn switch_listener(new: unsafe extern "C" fn()) {
    unsafe {
        CB = new;
        CB();
    }
}
