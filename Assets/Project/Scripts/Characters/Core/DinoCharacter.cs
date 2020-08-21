using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OhmsLibraries.Pooling;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

public class DinoCharacter : PoolMonoBehaviour {
    [ShowInInspector, HideInEditorMode]
    public bool CanMove {
        get => _moveRoutine != null;
        set {
            if ( _moveRoutine == null && value ) _moveRoutine = StartCoroutine( MoveRoutine() );
            if ( _moveRoutine != null && !value ) StopCoroutine( _moveRoutine );
        }
    }

    [SerializeField, Required]
    private Transform _playerBody;
    [SerializeField]
    private float _moveTime;
    [SerializeField, MinMaxSlider( 0f, 10f )]
    private Vector2 _moveInterval;
    [SerializeField]
    private Ease _moveEase;
    [SerializeField]
    private UnityEvent OnMoveStart, OnMoveEnd, OnSpawnComplete;

    private MoveArea _moveArea;
    private Tween _moveTween;
    private Coroutine _moveRoutine;

    private float RandomMoveInterval => Random.Range( _moveInterval.x, _moveInterval.y );

    public void Move ( Vector3 target ) {
#if UNITY_EDITOR
        Debug.DrawLine( transform.position, target, Color.red, _moveTime );
#endif        
        CheckFlip( target );

        _moveTween = transform.DOMove( target, _moveTime ).SetEase( _moveEase );
        _moveTween.OnStart( OnMoveStart.Invoke );
        _moveTween.OnComplete( () => {
            OnMoveEnd.Invoke();
            _moveTween = null;
        } );
        _moveTween.OnKill( () => _moveTween = null );
    }

    public void Stop () {
        if ( _moveTween != null ) _moveTween.Kill();
    }

    public void Spawn ( Vector3 position, MoveArea area ) {
        _moveArea = area;
        Spawn( position );
        OnMoveEnd.AddListener( FirstMove );
        Move( area.RandomLerp() );
    }

    private void CheckFlip ( Vector3 target ) {
        var direction = target - transform.position;
        var flip = direction.x > 0 ? 1 : -1;
        var scale = _playerBody.localScale;
        scale.x = flip * Mathf.Abs( scale.x );
        _playerBody.localScale = scale;
    }

    private IEnumerator MoveRoutine () {
        while ( true ) {
            yield return new WaitForSeconds( RandomMoveInterval );
            Move( _moveArea.RandomLerp() );
        }
    }

    private void FirstMove () {
        OnMoveEnd.RemoveListener( FirstMove );
        OnSpawnComplete.Invoke();
        CanMove = true;
    }
}
