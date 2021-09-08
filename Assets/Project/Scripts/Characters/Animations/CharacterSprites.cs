using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu( fileName = "Sprites", menuName = "Character Sprites" )]
public class CharacterSprites : ScriptableObject {


    [SerializeField, ValidateInput( "ValidateAnimation" ), HideLabel, BoxGroup( "Idle Animation" )]
    private SpriteAnimation _idleAnimation;
    [SerializeField, ValidateInput( "ValidateAnimation" ), BoxGroup( "Move Animation" )]
    private SpriteAnimation _moveAnimation;
    [SerializeField, ValidateInput( "ValidateAnimation" ), BoxGroup( "Move Animation" )]
    private SpriteAnimation _runAnimation;
    [SerializeField, ValidateInput( "ValidateAnimation" ), BoxGroup( "Attack Animation" )]
    private SpriteAnimation _attackAnimation;
    [SerializeField, ValidateInput( "ValidateAnimation" ), BoxGroup( "Attack Animation" )]
    private SpriteAnimation _hurtAnimation;

    public SpriteAnimation GetAnimation ( CharacterState state ) {
        SpriteAnimation anim = state switch {
            CharacterState.Moving => _moveAnimation,
            CharacterState.Attacking => _attackAnimation,
            CharacterState.Idle => _idleAnimation,
            CharacterState.Running => _runAnimation,
            CharacterState.Hurting => _hurtAnimation,
            _ => throw new System.NotImplementedException(),
        };
        return anim;

    }

    public Sprite Cycle ( CharacterState state, System.Action<CharacterState> onCycleCompleted, ref int currentSprite ) {
        currentSprite++;
        var currentSprites = GetAnimation( state ).Sprites;
        if ( currentSprite >= currentSprites.Length ) {
            currentSprite = 0;
            onCycleCompleted?.Invoke( state );
        }
        return currentSprites[currentSprite];
    }

#if UNITY_EDITOR
    private bool ValidateArray ( Sprite[] array ) {
        return array != null && array.Length > 0;
    }

    private bool ValidateAnimation ( SpriteAnimation anim ) {
        return ValidateArray( anim.Sprites );
    }

#endif
}

public enum CharacterState {
    Idle, Moving, Attacking, Running, Hurting
}
