using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OhmsLibraries.Pooling;
using UnityEngine.Events;
using DG.Tweening;

public class DinoCharacter : PoolMonoBehaviour {
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private Ease _moveEase;
    [SerializeField]
    private UnityEvent OnMoveStart, OnMoveEnd;

    private MoveArea _moveArea;
    public void Move ( Vector3 target ) {
        var tween = transform.DOMove( target, _moveTime ).SetEase( _moveEase );
        tween.OnStart( OnMoveStart.Invoke );
        tween.OnComplete( OnMoveEnd.Invoke );
    }

    public void Spawn( Vector3 position, MoveArea area ) {
        _moveArea = area;
        Spawn( position );
    }
}
