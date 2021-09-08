using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OhmsLibraries.Pooling;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CharacterMovement : MonoBehaviour {

    [SerializeField, Required]
    private Transform _playerBody;
    [SerializeField]
    private float _moveTime;
    [SerializeField, MinMaxSlider( 0f, 10f )]
    private Vector2 _moveInterval;
    [SerializeField]
    private Ease _moveEase;
    [SerializeField]
    private UnityMovementEvent _onMoveStart, _onMoveEnd;
    [SerializeField]
    private UnityEvent _onSpawnComplete;
    [SerializeField]
    private float _maxDistance = 4f;

    private Tween _moveTween;
    private Coroutine _moveRoutine, _resumeRoutine;
    public MoveArea MovingLine { get; private set; }
    public float MoveTime => _moveTime;

    [ShowInInspector, HideInEditorMode]
    public bool RandomMove {
        get => _moveRoutine != null;
        set {
            if ( _moveRoutine == null && value ) _moveRoutine = StartCoroutine( MoveRoutine() );
            if ( _moveRoutine != null && !value ) StopCoroutine( _moveRoutine );
        }
    }

    public bool MovingOut { get; private set; }

    private float RandomMoveInterval => Random.Range( _moveInterval.x, _moveInterval.y );

    public void Move ( Vector3 target, System.Action onMoveEnd = null ) {
        if ( _moveTween != null ) {
            _moveTween.Kill( false );
        }
#if UNITY_EDITOR
        Debug.DrawLine( transform.position, target, Color.red, _moveTime );
        //Debug.Log( $"Moving to {target}" );
#endif
        CheckFlip( target );
        var origin = transform.position;
        _moveTween = transform.DOMove( target, _moveTime ).SetEase( _moveEase );
        _moveTween.OnStart( () => _onMoveStart.Invoke( origin, target ) ); ;
        _moveTween.OnComplete( () => {
            _onMoveEnd.Invoke( origin, target );
            onMoveEnd?.Invoke();
            _moveTween = null;
        } );
        _moveTween.OnKill( () => _moveTween = null );
    }

    public void Stop () {
        if ( _moveTween != null ) _moveTween.Kill();
    }
    public void OnSpawn ( MoveArea area, bool randomMove = true ) {
        OnSpawn( area.RandomLerp(), area, randomMove );
    }

    public void OnSpawn ( Vector3 target, MoveArea area, bool randomMove = true ) {
        MovingLine = area;
        Move( target, () => {
            _onSpawnComplete.Invoke();
            RandomMove = randomMove;
        } );
    }

    private void Exit () {
        MovingOut = false;
    }

    public void MoveOut ( System.Action onMoveOut ) {
        MovingOut = true;
        Move( MovingLine.Lerp( -1 ), () => { onMoveOut.Invoke(); Exit(); } );
    }

    public void LookAt ( Vector3 target ) {
        CheckFlip( target );
    }

    public void ContinueRandomMove ( float delay ) {
        if ( _resumeRoutine != null ) StopCoroutine( _resumeRoutine );
        _resumeRoutine = StartCoroutine( ResumeRandomMove( delay ) );
    }

    public void StopDespawn () {
        if ( MovingOut ) {
            _moveTween.Kill();
            MovingOut = false;
            RandomMove = true;
        }
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
            var target = MovingLine.RandomClampedLerp( transform.position, _maxDistance );
            Move( target );
        }
    }

    private IEnumerator ResumeRandomMove ( float duration ) {
        yield return new WaitForSeconds( duration );
        RandomMove = true;
        _moveRoutine = null;
    }
}
