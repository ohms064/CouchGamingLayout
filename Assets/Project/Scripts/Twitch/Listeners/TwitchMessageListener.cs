using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchMessageListener : TwitchClientEventListener {
    public override void Listen ( Client connection ) {
        connection.OnMessageReceived += Twitch_MessageReceived;
    }

    private void Twitch_MessageReceived ( object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e ) {
        Debug.Log( "Received mesage!" );
    }
}
