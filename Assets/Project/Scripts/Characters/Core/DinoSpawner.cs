using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DinoSpawner : MonoBehaviour {
    [SerializeField, Required]
    private DinoPool _pool;
    [SerializeField, Required]
    private NameTagManager _tagManager;
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
    public DinoCharacter Spawn ( string avatarName ) {
        if ( _pool.RequestPoolMonoBehaviour( out DinoCharacter dinoCharacter ) ) {
            _tagManager.AddTag( dinoCharacter.NameTag );
            dinoCharacter.Spawn( _origin.position, _area, avatarName, GetPriority() );
            return dinoCharacter;
        }
        return null;
    }

    public DinoCharacter Spawn ( string avatarName, Vector3 target, bool randomMove = true ) {
        if ( _pool.RequestPoolMonoBehaviour( out DinoCharacter dinoCharacter ) ) {
            _tagManager.AddTag( dinoCharacter.NameTag );
            dinoCharacter.Spawn( _origin.position, target, _area, avatarName, GetPriority(), randomMove );
            return dinoCharacter;
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
public class UnityDinoEvent : UnityEvent<DinoCharacter> { }