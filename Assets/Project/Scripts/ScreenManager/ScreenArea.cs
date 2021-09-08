using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ScreenArea : MonoBehaviour {
    [SerializeField, Required]
    private Camera _camera;
    [SerializeField, Required]
    private MoveArea _moveArea;
    [SerializeField]
    private Vector3 _startViewportPoint, _endViewportPoint;
    [SerializeField]
    private UnityPointEvent _onStartUpdated, _onEndUpdated;

    private void Awake () {
        var startPoint = GetStartPoint();
        var endPoint = GetEndPoint(); ;
        _moveArea.ForceUpdateLine( startPoint, endPoint );
        _onStartUpdated.Invoke( startPoint );
        _onEndUpdated.Invoke( endPoint );
    }

    private void FixedUpdate () {
        var startPoint = GetStartPoint();
        var endPoint = GetEndPoint();

        if ( !_moveArea.UpdateLine( startPoint, endPoint ) ) return;

        _onStartUpdated.Invoke( startPoint );
        _onEndUpdated.Invoke( endPoint );
    }

    private Vector3 GetStartPoint () {
        var startPoint = _camera.ViewportToWorldPoint( _startViewportPoint );
        startPoint.z = _startViewportPoint.z;
        return startPoint;
    }

    private Vector3 GetEndPoint () {
        var endPoint = _camera.ViewportToWorldPoint( _endViewportPoint );
        endPoint.z = _endViewportPoint.z;
        return endPoint;
    }
}

[System.Serializable]
public class UnityPointEvent : UnityEvent<Vector3> { }
