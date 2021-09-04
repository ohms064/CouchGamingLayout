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

    private void FixedUpdate () {
        var startPoint = _camera.ViewportToWorldPoint( _startViewportPoint );
        var endPoint = _camera.ViewportToWorldPoint( _endViewportPoint );

        startPoint.z = _startViewportPoint.z;
        endPoint.z = _endViewportPoint.z;

        _moveArea.UpdateLine( startPoint, endPoint );
        _onStartUpdated.Invoke( startPoint );
        _onEndUpdated.Invoke( endPoint );
    }
}
[System.Serializable]
public class UnityPointEvent : UnityEvent<Vector3> { }
