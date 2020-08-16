using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea {
    [SerializeField]
    private Vector3 start, end;

    public Vector3 Lerp ( float t ) {
        return Vector3.Lerp( start, end, t );
    }

    public Vector3 RandomLerp () {
        return Lerp( Random.Range( 0f, 1f ) );
    }
}
