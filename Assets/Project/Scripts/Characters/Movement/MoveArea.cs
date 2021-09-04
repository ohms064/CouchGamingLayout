using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;

[CreateAssetMenu( fileName = "Moving Area", menuName = "Moving Area" )]
public class MoveArea : ScriptableObject {
    [SerializeField]
    private Vector3 _start, _end;

    [System.NonSerialized]
    public Vector3 origin;

    public Vector3 Center => Lerp( 0.5f );

    private Vector3 Start => origin + _start;
    private Vector3 End => origin + _end;

    public Vector3 Lerp ( float t ) {
        return origin + Vector3.Lerp( _start, _end, t );
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

    public void UpdateLine(Vector3 start, Vector3 end ) {
        _start = start;
        _end = end;
    }

#if UNITY_EDITOR
    public void Editor_DrawGizmos ( Vector3 origin ) {
        Draw.LineThickness = 0.05f;
        Draw.Color = Color.blue;
        Draw.Line( origin + _start, origin + _end );
    }
#endif
}
