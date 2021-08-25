using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
public class TwitchChatRespond : TwitchClientEventListener {
    [Required, SerializeField]
    private DinoSpawner _spawner;
    [Required, SerializeField]
    private TwitchChatPool _chatUsers;
    [SerializeField]
    private float _talkingDistance = 0.2f;
    [SerializeField]
    private float _chatTime = 1.2f;

    public override void Listen ( Client connection ) {
        connection.OnMessageReceived += Twitch_OnMessageReceived;
    }

    private void Twitch_OnMessageReceived ( object sender, OnMessageReceivedArgs e ) {
        if ( !HasReply( _chatUsers, e.ChatMessage ) ) return;
        Debug.Log( $"Chat respond? responding to: {e.ChatMessage.ChatReply?.ParentDisplayName ?? "Nobody"}" );
        //TODO: Find a better way to do who takes control over the message.
        var respondingTo = _chatUsers[e.ChatMessage.ChatReply.ParentUserId];
        if ( _chatUsers.ContainsKey( e.ChatMessage.UserId ) ) {
            var responder = _chatUsers[e.ChatMessage.UserId];
            var centerPoint = ( responder.transform.position + respondingTo.transform.position ) / 2f;
            var direction = respondingTo.transform.position - responder.transform.position;

            if ( direction.x < 0 ) {
                MoveToCenter( respondingTo, responder, centerPoint );
            }
            else {
                MoveToCenter( responder, respondingTo, centerPoint );
            }
        }
        else {
            var direction = respondingTo.transform.position - respondingTo.MovingLine.Center;
            var target = respondingTo.transform.position + ( _talkingDistance *
                ( direction.x < 0 ?
                respondingTo.MovingLine.DirectionToEnd( respondingTo.transform.position ) :
                respondingTo.MovingLine.DirectionToStart( respondingTo.transform.position ) ) );
            Debug.Log( $"Moving spawned to: {target}" );
            var avatar = _spawner.Spawn( e.ChatMessage.Username, target, false );
            avatar.ContinueRandomMove( _chatTime + avatar.MoveTime );
            _chatUsers.Add( e.ChatMessage.UserId, avatar );
        }
    }

    public static bool HasReply ( TwitchChatPool chatUsers, ChatMessage chatMessage ) {
        return chatMessage.ChatReply != null && chatUsers.ContainsKey( chatMessage.ChatReply.ParentUserId );
    }

    private void MoveToCenter ( DinoCharacter left, DinoCharacter right, Vector3 centerPoint ) {
        var leftPoint = centerPoint + ( left.MovingLine.DirectionToStart( centerPoint ) * _talkingDistance );
        var rightPoint = centerPoint + ( right.MovingLine.DirectionToStart( centerPoint ) * _talkingDistance );
        Debug.Log( $"Center Point: {centerPoint} leftPoint {leftPoint} rightPoint {rightPoint}" );
        left.RandomMove = false;
        right.RandomMove = false;
        left.Move( leftPoint, () => EndMovement( left, right.transform.position ) );
        right.Move( rightPoint, () => EndMovement( right, left.transform.position ) );
    }

    private void EndMovement ( DinoCharacter character, Vector3 lookAt ) {
        character.LookAt( lookAt );
        character.ContinueRandomMove( _chatTime );
    }

    private void OnDestroy () {
        _chatUsers.Clear();
    }
}
