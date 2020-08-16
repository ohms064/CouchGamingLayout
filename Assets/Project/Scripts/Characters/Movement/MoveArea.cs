using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
[System.Serializable]
public class MoveArea {
    [SerializeField]
    private Vector3 start, end;

    [System.NonSerialized]
    public Vector3 origin;

    public Vector3 Lerp ( float t ) {
        return origin + Vector3.Lerp( start, end, t );
    }

    public Vector3 RandomLerp () {
        return Lerp( Random.Range( 0f, 1f ) );
    }

#if UNITY_EDITOR
    public void Editor_DrawGizmos ( Vector3 origin ) {
        Draw.LineThickness = 0.05f;
        Draw.Color = Color.blue;
        Draw.Line( origin + start, origin + end );
    }
#endif
}
