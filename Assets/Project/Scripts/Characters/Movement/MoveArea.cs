using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;

[System.Serializable]
public class MoveArea {
    [SerializeField, OnValueChanged( "OnStartEndChanged" )]
    private Vector3 _start, _end;
    [SerializeField, DisableInPlayMode, DisableInEditorMode]
    private float _magnitude;
    [SerializeField, DisableInPlayMode, DisableInEditorMode]
    private Vector3 _direction;

    [System.NonSerialized]
    public Vector3 origin;

    public Vector3 Center => Lerp( 0.5f );

    private Vector3 Start => origin + _start;
    private Vector3 End => origin + _end;

    public Vector3 Lerp ( float t ) {
        return origin + Vector3.Lerp( _start, _end, t );
    }

    public float Project ( Vector3 position ) {
        var direction = position - Start;
        return Vector3.Dot( _direction, direction );
    }

    public Vector3 DirectionToEnd ( Vector3 position ) {
        return ( End - position ).normalized;
    }

    public Vector3 DirectionToStart ( Vector3 position ) {
        return ( Start - position ).normalized;
    }

    public Vector3 RandomLerp () {
        return Lerp( Random.Range( 0f, 1f ) );
    }

#if UNITY_EDITOR
    public void Editor_DrawGizmos ( Vector3 origin ) {
        Draw.LineThickness = 0.05f;
        Draw.Color = Color.blue;
        Draw.Line( origin + _start, origin + _end );
    }

    private void OnStartEndChanged () {
        _direction = _end - _start;
        _magnitude = _direction.magnitude;
        _direction /= _magnitude;
    }
#endif
}
