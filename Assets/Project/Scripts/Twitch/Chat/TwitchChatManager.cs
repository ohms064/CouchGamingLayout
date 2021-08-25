using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class TwitchChatManager : TwitchClientEventListener {

    [SerializeField]
    private DinoSpawner _spawner;
    [SerializeField]
    private float _maxIdleTime;

    [SerializeField, Required]
    private TwitchChatPool _chatUsers;

    public override void Listen ( Client connection ) {
        connection.OnMessageReceived += Twitch_MessageReceived;
    }

    private void Twitch_MessageReceived ( object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e ) {
        Debug.Log( $"Message from {e.ChatMessage.Username}, {e.ChatMessage.Message}, emotes {e.ChatMessage.EmoteSet.Emotes.FirstOrDefault()?.ImageUrl ?? "None"}" );
        //if ( TwitchChatRespond.HasReply( _chatUsers, e.ChatMessage ) ) {
        //    Debug.Log( "Chat Has reply" );
        //    return;
        //}
        if ( _chatUsers.ContainsKey( e.ChatMessage.UserId ) ) {
            _chatUsers[e.ChatMessage.UserId].TwitchUser.UpdateMessage( e.ChatMessage.Message );
            _chatUsers[e.ChatMessage.UserId].TwitchUser.TalkingTo( e.ChatMessage.ChatReply?.ParentUserId ?? null );
        }
        else {
            var avatar = _spawner.Spawn( e.ChatMessage.Username );
            _chatUsers.Add( e.ChatMessage.UserId, avatar );
        }
    }

    private void FixedUpdate () {
        foreach ( var item in _chatUsers ) {
            item.Value.TwitchUser.UpdateTime( Time.fixedDeltaTime );
            if ( item.Value.TwitchUser.Time >= _maxIdleTime && !item.Value.MovingOut ) {
                item.Value.MoveOut( () => _chatUsers.Remove( item.Key ) );
            }
        }
    }
}