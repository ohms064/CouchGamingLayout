using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSpawner : MonoBehaviour {
    [SerializeField, Required]
    private DinoPool _pool;
    [SerializeField, Required]
    private Transform _origin;
    [SerializeField]
    private MoveArea _area;

    private void Awake () {
        _area.origin = transform.position;
    }

    [Button, HideInEditorMode]
    public DinoCharacter Spawn ( string avatarName ) {
        if ( _pool.RequestPoolMonoBehaviour( out DinoCharacter dinoCharacter ) ) {
            dinoCharacter.Spawn( _origin.position, _area, avatarName );
            return dinoCharacter;
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos () {
        _area?.Editor_DrawGizmos( transform.position );
    }
#endif
}
