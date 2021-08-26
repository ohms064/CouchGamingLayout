using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OhmsLibraries.Controllers;
using Sirenix.OdinInspector;
using Shapes;

public class NameTag : MonoBehaviour {
    [SerializeField, Required]
    private TextMeshPro _textMesh;
    [SerializeField, Required]
    private Transform _follow;
    [SerializeField, Required]
    private BoxCollider _collider;
    [SerializeField]
    private PIDVector3Controller _positionController;

    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    public int Priority { get; private set; }
    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    public string Name { get => _textMesh.text; set => _textMesh.text = value; }
    [ShowInInspector, HideInEditorMode]
    public float Height { get; set; }
    public BoxCollider Collider => _collider;
    public int RowOffset { get; set; }
    public float Offset { get => RowOffset * Manager.OffsetDistance; }

    public NameTagManager Manager { get; set; }

    private Vector3 FollowPosition {
        get {
            var followPosition = _follow.position;
            followPosition.y = Height;
            return followPosition;
        }
    }

    public void OnSpawn ( int priority ) {
        transform.SetParent( null );
        Priority = priority;
        Height = Manager.BaseHeight;
        enabled = true;
    }

    public void OnDespawn () {
        enabled = false;
        transform.SetParent( _follow );
        Manager.RemoveTag( this );
    }

    private void FixedUpdate () {
        PIDMove();
    }

    private void PIDMove () {
        DebugGraph.Log( $"{_follow.name} FollowPosition", FollowPosition );
        DebugGraph.Log( $"{_follow.name} Position", transform.position );
        DebugGraph.Log( $"{_follow.name} Height", Height );
        var position = transform.position;
        position += _positionController.Seek( FollowPosition, position, Time.fixedDeltaTime );
        position.y = Height;
        transform.position = position;
    }

#if UNITY_EDITOR
    private void Reset () {
        _follow = transform.parent;
        _ = TryGetComponent( out _textMesh );
        enabled = false;
        _ = TryGetComponent( out _collider );
    }

    private void OnDrawGizmos () {
        if ( !UnityEditor.EditorApplication.isPlaying ) return;
        Draw.Color = Color.green;
        Draw.LineThickness = 0.05f;
        Draw.Line( transform.position, FollowPosition );
        Draw.Text( transform.position + Vector3.up * 0.5f, $"P:{Priority} RO:{RowOffset}", 4 );
        Draw.Line( transform.position, transform.position + ( Vector3.down * Manager.CastDistance ) );
    }
#endif
}
