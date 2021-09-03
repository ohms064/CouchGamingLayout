using OhmsLibraries.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : PoolMonoBehaviour {
    [SerializeField]
    private CharacterMovement _movement;
    [SerializeField]
    private MessageToEmotion _emotes;
    [SerializeField]
    private CharacterChatManager _chatManager;
    [SerializeField]
    private NameTag _tag;

    public float Time { get; private set; }

    public CharacterMovement Movement => _movement;
    public MessageToEmotion Emotes => _emotes;
    public NameTag Tag => _tag;
    public CharacterChatManager ChatManager => _chatManager;

    public void Spawn ( Vector3 position, Vector3 target, MoveArea area, CharacterChatMessage message, int priority, bool randomMove = true ) {
        base.Spawn( position );
        Time = 0;
        Movement.OnSpawn( target, area, randomMove );
        ChatManager.ReceiveMessage( message );
        Tag.OnSpawn( priority );
    }

    public void Spawn ( Vector3 position, MoveArea area, CharacterChatMessage message, int priority, bool randomMove = true ) {
        base.Spawn( position );
        Time = 0;
        Movement.OnSpawn( area, randomMove );
        ChatManager.ReceiveMessage( message );
        Tag.OnSpawn( priority );
    }

    public void UpdateTime ( float t ) {
        Time += t;
    }
}
