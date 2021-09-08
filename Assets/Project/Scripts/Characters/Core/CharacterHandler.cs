using OhmsLibraries.Pooling;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterHandler : PoolMonoBehaviour {
    [SerializeField, Required]
    private CharacterMovement _movement;
    [SerializeField, Required]
    private MessageToEmotion _emotes;
    [SerializeField, Required]
    private CharacterChatManager _chatManager;
    [SerializeField, Required]
    private NameTag _tag;
    [SerializeField, Required]
    private EmojiParticles _emoji;

    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    public float Time { get; private set; }

    public CharacterMovement Movement => _movement;
    public MessageToEmotion Emotes => _emotes;
    public NameTag Tag => _tag;
    public CharacterChatManager ChatManager => _chatManager;
    public EmojiParticles Emojis => _emoji;

    public void Spawn ( Vector3 position, Vector3 target, MoveArea area, TwitchEmoteManager emoteManager, CharacterChatMessage message, int priority, bool randomMove = true ) {
        base.Spawn( position );
        Time = 0;
        Movement.OnSpawn( target, area, randomMove );
        ChatManager.ReceiveMessage( message );
        Tag.OnSpawn( priority, message.UserName );
        Emojis.OnSpawn( emoteManager );
    }

    public void Spawn ( Vector3 position, MoveArea area, TwitchEmoteManager emoteManager, CharacterChatMessage message, int priority, bool randomMove = true ) {
        base.Spawn( position );
        Time = 0;
        Movement.OnSpawn( area, randomMove );
        ChatManager.ReceiveMessage( message );
        Tag.OnSpawn( priority, message.UserName );
        Emojis.OnSpawn( emoteManager );
    }

    public void UpdateTime ( float t ) {
        Time += t;
    }

    public void CancelDespawn () {
        Movement.StopDespawn();
        Time = 0f;
    }

    public void StartDespawn ( System.Action onDespawn ) {
        Movement.MoveOut( () => {
            onDespawn?.Invoke();
            Despawn();
        } );
    }

    public override void Despawn () {
        Tag.OnDespawn();
        base.Despawn();
    }
}
