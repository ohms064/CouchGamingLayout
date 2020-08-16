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
    public void Spawn () {
        if ( _pool.RequestPoolMonoBehaviour( out DinoCharacter dinoCharacter ) ) {
            dinoCharacter.Spawn( _origin.position, _area );
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos () {
        _area?.Editor_DrawGizmos( transform.position );
    }
#endif
}
