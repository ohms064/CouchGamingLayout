using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TwitchLib.Client.Events;
public class TwitchChatManager : TwitchClientEventListener {

    [SerializeField]
    private CharacterSpawner _spawner;
    [SerializeField]
    private float _maxIdleTime;
    [SerializeField, Required]
    private TwitchChatPool _chatUsers;

    public override void Listen ( Client connection ) {
        connection.OnMessageReceived += Twitch_MessageReceived;
    }

    private void Twitch_MessageReceived ( object sender, OnMessageReceivedArgs e ) {
        Debug.Log( $"Message from {e.ChatMessage.Username}, {e.ChatMessage.Message}, emotes {e.ChatMessage.EmoteSet.Emotes.FirstOrDefault()?.ImageUrl ?? "None"}" );

        _chatUsers.Updated = false;
        var chatMessage = BuildCharacterChatMessage( e );
        if ( _chatUsers.ContainsKey( e.ChatMessage.UserId ) ) {
            _chatUsers[e.ChatMessage.UserId].ChatManager.ReceiveMessage( chatMessage );
        }
        else {
            var avatar = _spawner.Spawn( chatMessage );
            _chatUsers.Add( e.ChatMessage.UserId, avatar );
        }
        _chatUsers.Updated = true;
    }

    private void FixedUpdate () {
        foreach ( var item in _chatUsers ) {
            item.Value.UpdateTime( Time.fixedDeltaTime );
            if ( item.Value.Time >= _maxIdleTime && !item.Value.Movement.MovingOut ) {
                item.Value.StartDespawn( () => _chatUsers.Remove( item.Key ) );
            }
        }
    }

    private CharacterChatMessage BuildCharacterChatMessage ( OnMessageReceivedArgs args ) {
        var chatMessage = args.ChatMessage;
        var emoteSet = chatMessage.EmoteSet;
        var chatReply = chatMessage.ChatReply;
        CharacterChatMessage parentMessage = null;
        if ( chatReply != null ) {
            parentMessage = new CharacterChatMessage( chatReply.ParentMsgBody, chatReply.ParentDisplayName, chatReply.ParentUserId, _chatUsers, emoteSet, null );
        }
        return new CharacterChatMessage( chatMessage.Message, chatMessage.DisplayName, chatMessage.UserId, _chatUsers, emoteSet, parentMessage );
    }
}