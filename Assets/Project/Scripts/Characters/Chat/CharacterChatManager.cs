using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChatManager : MonoBehaviour {
    [SerializeField]
    private CharacterChatListener[] _listeners;

    public CharacterChatMessage LastMessage { get; private set; }

    public void ReceiveMessage ( CharacterChatMessage message ) {
        LastMessage = message;
        message.Chatters.RegisterOnUpdated( OnChattersUpdated );
    }

    private void OnChattersUpdated ( TwitchChatPool pool ) {
        pool.UnregisterOnUpdated( OnChattersUpdated );
        foreach ( var listener in _listeners ) {
            if ( !listener.CheckMessage( LastMessage ) ) continue;
            listener.ManageMessage( LastMessage );
        }
    }
}
