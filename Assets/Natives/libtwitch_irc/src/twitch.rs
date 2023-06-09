use std::sync::mpsc::Receiver;
use tokio::runtime;
use twitch_irc::{
    login::StaticLoginCredentials as LoginCreds,
    message::ServerMessage::{self, Privmsg},
    ClientConfig as Config, SecureTCPTransport as TCPTransport, TwitchIRCClient as IRC,
};

pub const SHUTDOWN_KEY: &str = "FORCE_SHUTDOWN";

pub fn fake_main(receiver: Receiver<String>) {
    let r = runtime::Runtime::new().unwrap();

    println!("fake main");

    r.block_on(async move {
        println!("block time");

        let cfg = Config::default();
        let (mut incoming, client) = IRC::<TCPTransport, LoginCreds>::new(cfg);

        println!("make handle");

        let twitch_recv_handle = tokio::spawn(async move {
            while let Some(msg) = incoming.recv().await {
                //                  println!("Recieved: {:#?}", msg);

                let Privmsg(msg) = msg else {
                    continue;
                };
                println!("new msssg: {}", msg.message_text);

                //                println!("New Message: {}", msg.message_text);

                unsafe {
                    CB();
                }
            }
        });

        println!("done make hand");

        //        std::thread::sleep(std::time::Duration::from_secs(3));
        client.join("koolieaid".into()).unwrap();

        println!("start recv loop");
        // I have no clue how to stop this LMAO
        while let Ok(ch) = receiver.recv() {
            // might remove
            //            if ch == SHUTDOWN_KEY {
            //                break;
            //            }

            match client.join(ch) {
                Ok(_) => {}
                Err(e) => eprintln!("Wrong format! E: {e}"),
            }
        }

        drop(client);
        twitch_recv_handle.await.unwrap();

        println!("end loop");

        // Should be last since this blocks the thread
        //        twitch_recv_handle.await.unwrap();
    });

    println!("outside block");
}

extern "C" fn empty_listener() {}

static mut CB: extern "C" fn() -> () = empty_listener;

pub fn switch_listener(new: extern "C" fn()) {
    unsafe {
        CB = new;
        CB();
    }
}
