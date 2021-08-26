using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagManager : MonoBehaviour {

    [SerializeField, Required]
    private TwitchChatPool _chatPool;
    [SerializeField]
    private LayerMask _mask;
    [SerializeField]
    private float _castDistance = 1.3f;
    [SerializeField]
    private float _offsetDistance = 1f;
    [SerializeField]
    private float _baseHeight = 5f;

    public float OffsetDistance => _offsetDistance;
    public float BaseHeight => _baseHeight;
    public float CastDistance => _castDistance;

    [ShowInInspector, HideInEditorMode]
    private List<NameTag> _activeNameTags = new List<NameTag>();

    public void AddTag ( NameTag tag ) {
        _activeNameTags.Add( tag );
        tag.Manager = this;
    }

    public void RemoveTag ( NameTag tag ) {
        _activeNameTags.Remove( tag );
    }

    private void FixedUpdate () {
        //Check if tags are colliding
        if ( _activeNameTags.Count < 2 ) return;
        foreach ( var tag in _activeNameTags ) {
            CheckRaycast( tag );
        }

    }
    private void CheckRaycast ( NameTag tag ) {
        var hits = new RaycastHit[_chatPool.Count];
        var count = Physics.BoxCastNonAlloc( tag.transform.position, tag.Collider.bounds.extents * 0.5f,
            Vector3.down, hits, Quaternion.identity, _castDistance, _mask );
        if ( count <= 1 ) { //It only hit itself or nothing
            Debug.Log( "Hit!" );
            if ( tag.RowOffset > 0 ) {
                tag.RowOffset--;
                tag.Height = _baseHeight + tag.Offset;
            }
            return;
        }
        for ( int i = 0; i < count; i++ ) {
            hits[i].transform.TryGetComponent( out NameTag otherTag );
            if ( otherTag == tag || otherTag.Priority > tag.Priority || hits[i].distance >= _offsetDistance || otherTag.RowOffset < tag.RowOffset ) continue;
            tag.RowOffset = otherTag.RowOffset + 1;
            tag.Height = _baseHeight + tag.Offset;
            return;
        }
    }
}
