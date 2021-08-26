using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OhmsLibraries.Pooling;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

public class DinoCharacter : PoolMonoBehaviour {

    [SerializeField, Required]
    private Transform _playerBody;
    [SerializeField, Required]
    private TwitchActiveUser _twitchUser;
    [SerializeField, Required]
    private NameTag _tag;
    [SerializeField]
    private float _moveTime;
    [SerializeField, MinMaxSlider( 0f, 10f )]
    private Vector2 _moveInterval;
    [SerializeField]
    private Ease _moveEase;
    [SerializeField]
    private UnityEvent OnMoveStart, OnMoveEnd, OnSpawnComplete;

    private Tween _moveTween;
    private Coroutine _moveRoutine, _resumeRoutine;

    public MoveArea MovingLine { get; private set; }
    public NameTag NameTag => _tag;
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

    public TwitchActiveUser TwitchUser => _twitchUser;

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

        _moveTween = transform.DOMove( target, _moveTime ).SetEase( _moveEase );
        _moveTween.OnStart( OnMoveStart.Invoke );
        _moveTween.OnComplete( () => {
            OnMoveEnd.Invoke();
            onMoveEnd?.Invoke();
            _moveTween = null;
        } );
        _moveTween.OnKill( () => _moveTween = null );
    }

    public void Stop () {
        if ( _moveTween != null ) _moveTween.Kill();
    }

    public void Spawn ( Vector3 position, MoveArea area, string avatarName, int priority, bool randomMove = true ) {
        Spawn( position, area.RandomLerp(), area, avatarName, priority, randomMove );
    }

    public void Spawn ( Vector3 position, Vector3 target, MoveArea area, string avatarName, int priority, bool randomMove = true ) {
        MovingLine = area;
        Spawn( position );
        Move( target, () => {
            OnSpawnComplete.Invoke();
            RandomMove = randomMove;
        } );
        TwitchUser.Reset( avatarName );
        name = $"dino_{avatarName}";
        _tag.Name = avatarName;
        _tag.OnSpawn( priority );
    }

    public override void Despawn () {
        base.Despawn();
        MovingOut = false;
        _tag.OnDespawn();
    }

    public void MoveOut ( System.Action onMoveOut ) {
        MovingOut = true;
        Move( MovingLine.Lerp( -1 ), () => { onMoveOut.Invoke(); Despawn(); } );
    }

    public void LookAt ( Vector3 target ) {
        CheckFlip( target );
    }

    public void ContinueRandomMove ( float delay ) {
        if ( _resumeRoutine != null ) StopCoroutine( _resumeRoutine );
        _resumeRoutine = StartCoroutine( ResumeRandomMove( delay ) );
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
            Move( MovingLine.RandomLerp() );
        }
    }

    private IEnumerator ResumeRandomMove ( float duration ) {
        yield return new WaitForSeconds( duration );
        RandomMove = true;
        _moveRoutine = null;
    }
}
