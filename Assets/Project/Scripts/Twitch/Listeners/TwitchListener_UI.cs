using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;
using Doozy.Engine;
public class TwitchListener_UI : TwitchClientEventListener
{
    public override void Listen ( Client connection ) {
        connection.OnConnected += Twitch_OnClientConnected;
        connection.OnDisconnected += Twitch_OnClientDisconnected;
    }

    private void Twitch_OnClientDisconnected ( object sender, TwitchLib.Communication.Events.OnDisconnectedEventArgs e ) {
        GameEventMessage.SendEvent( "Return" );
        
    }

    private void Twitch_OnClientConnected ( object sender, TwitchLib.Client.Events.OnConnectedArgs e ) {
        GameEventMessage.SendEvent( "OnSuccessLogin" );
    }
}
