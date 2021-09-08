using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterChatManager : MonoBehaviour {
    [SerializeField]
    private CharacterChatListener[] _listeners;
    [SerializeField]
    private UnityEvent _onMessageReceived;

    public CharacterChatMessage LastMessage { get; private set; }

    public void ReceiveMessage ( CharacterChatMessage message ) {
        LastMessage = message;
        message.Chatters.RegisterOnUpdated( OnChattersUpdated );
        _onMessageReceived.Invoke();
    }

    private void OnChattersUpdated ( TwitchChatPool pool ) {
        pool.UnregisterOnUpdated( OnChattersUpdated );
        foreach ( var listener in _listeners ) {
            if ( !listener.CheckMessage( LastMessage ) ) continue;
            listener.ManageMessage( LastMessage );
        }
    }
}
