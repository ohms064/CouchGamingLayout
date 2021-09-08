using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {
    [SerializeField]
    private CharacterSprites _animationSprites;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private float _runDistance;
    [SerializeField]
    private CharacterState _defaultAnimation;

    private CharacterState _currentState;
    private int _currentSprite;
    private event System.Action<CharacterState> _onCycleCompleted;
    private float _currentTime;

    private float SecondsRate => _animationSprites.GetAnimation( _currentState ).ChangeRate;

    public void StartMoving ( Vector3 origin, Vector3 target ) {
        var distance = ( target - origin ).sqrMagnitude;
        if ( distance < _runDistance * _runDistance )
            ChangeCharacterState( CharacterState.Moving );
        else
            ChangeCharacterState( CharacterState.Running );
    }

    public void BeginIdle () {
        ChangeCharacterState( CharacterState.Idle );
    }

    public void StartAttack () {
        ChangeCharacterState( CharacterState.Attacking );
    }

    public void StartHurting () {
        ChangeCharacterState( CharacterState.Hurting );
    }

    public void OnSpawn () {
        _currentState = _defaultAnimation;
        _currentSprite = 0;
        _currentTime = 0f;
        _renderer.sprite = _animationSprites.Cycle( _currentState, _onCycleCompleted, ref _currentSprite );
    }

    private void Update () {
        _currentTime += Time.deltaTime;
        if ( _currentTime < SecondsRate ) return;

        _currentTime -= SecondsRate;
        _renderer.sprite = _animationSprites.Cycle( _currentState, _onCycleCompleted, ref _currentSprite );
    }

    private void ChangeCharacterState ( CharacterState state ) {
        _currentState = state;
        _currentSprite = 0;
    }
}
