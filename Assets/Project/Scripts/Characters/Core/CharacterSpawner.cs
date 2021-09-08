using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSpawner : MonoBehaviour {
    [SerializeField, Required]
    private CharacterPool _pool;
    [SerializeField, Required]
    private NameTagManager _tagManager;
    [SerializeField, Required]
    private TwitchEmoteManager _emoteManager;
    [SerializeField, Required]
    private Transform _origin;
    [SerializeField]
    private MoveArea _area;
    [SerializeField]
    private UnityDinoEvent _onDinoSpawn;

    private int _dinoPriorityCount = 0;

    private void Awake () {
        _area.origin = transform.position;
    }

    [Button, HideInEditorMode]
    public CharacterHandler Spawn ( CharacterChatMessage chatMessage ) {
        if ( _pool.RequestPoolMonoBehaviour( out CharacterHandler characterHandler ) ) {
            _tagManager.AddTag( characterHandler.Tag );
            characterHandler.Spawn( _origin.position, _area, _emoteManager, chatMessage, GetPriority() );
            return characterHandler;
        }
        return null;
    }

    public CharacterHandler Spawn ( CharacterChatMessage chatMessage, Vector3 target, bool randomMove = true ) {
        if ( _pool.RequestPoolMonoBehaviour( out CharacterHandler character ) ) {
            _tagManager.AddTag( character.Tag );
            character.Spawn( _origin.position, target, _area, _emoteManager, chatMessage, GetPriority(), randomMove );
            return character;
        }
        return null;
    }

    private int GetPriority () {
        var returnValue = _dinoPriorityCount;
        _dinoPriorityCount = (int) Mathf.Repeat( _dinoPriorityCount + 1, _pool.poolSize );
        return returnValue;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos () {
        _area?.Editor_DrawGizmos( transform.position );
    }
#endif
}

[System.Serializable]
public class UnityDinoEvent : UnityEvent<CharacterMovement> { }