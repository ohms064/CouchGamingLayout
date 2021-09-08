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
    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    private float _distance;
    [ShowInInspector, DisableInEditorMode, DisableInPlayMode]
    private Vector3 _direction;

    public Vector3 Center => Lerp( 0.5f );

    public Vector3 Start => origin + _start;
    public Vector3 End => origin + _end;
    public float Distance => _distance;

    public Vector3 Lerp ( float t ) {
        return origin + Vector3.Lerp( _start, _end, t );
    }

    public float InverseLerp ( Vector3 value ) {
        Vector3 AB = End - Start;
        Vector3 AV = value - Start;
        return Vector3.Dot( AV, AB ) / Vector3.Dot( AB, AB );
    }

    public Vector3 RandomLerp () {
        return Lerp( Random.Range( 0f, 1f ) );
    }

    public Vector3 RandomClampedLerp ( Vector3 origin, float maxDistance ) {
        var tEnd = Mathf.Clamp01( InverseLerp( origin + _direction * maxDistance ) );
        var tStart = Mathf.Clamp01( InverseLerp( origin - _direction * maxDistance ) );
        return Lerp( Random.Range( tStart, tEnd ) );
    }

    public bool UpdateLine ( Vector3 start, Vector3 end ) {
        if ( start == _start || end == _end ) return false;

        ForceUpdateLine( start, end );
        return true;
    }

    public void ForceUpdateLine ( Vector3 start, Vector3 end ) {
        _start = start;
        _end = end;
        _distance = Vector3.Magnitude( _end - _start );
        _direction = ( end - start ).normalized;
    }

#if UNITY_EDITOR
    public void Editor_DrawGizmos ( Vector3 origin ) {
        Draw.LineThickness = 0.05f;
        Draw.Color = Color.blue;
        Draw.Line( origin + _start, origin + _end );
    }
#endif
}
