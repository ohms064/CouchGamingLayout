using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DinoTween : MonoBehaviour {

    [SerializeField]
    protected Transform target;

    private List<Tween> _activeTweens = new List<Tween>();

    protected SpriteRenderer spriteRenderer;

    private Color _initialColor;
    private Vector3 _initialLocalPosition;
    private Vector3 _initialLocalScale;

    private void Awake () {
        spriteRenderer = target.GetComponent<SpriteRenderer>();
        _initialColor = spriteRenderer.color;
        _initialLocalScale = target.localScale;
        _initialLocalPosition = target.localPosition;
    }

    [Button, DisableInEditorMode]
    public virtual void Appear ( DinoTweenData data, System.Action endCallback = null ) {
        foreach ( var activeTween in _activeTweens ) {
            activeTween.Kill();
        }
        _activeTweens.Clear();

        if ( data.Movement.Enabled ) {
            if ( !data.Movement.UsePreviousValue ) target.localPosition = data.Movement.InitialValue;
            var tween = target.DOLocalMove( data.Movement.EndValue, data.Movement.TweenDuration );
            ConfigureTween( tween, data.Movement );
        }

        if ( data.Scale.Enabled ) {
            if ( !data.Scale.UsePreviousValue ) target.localScale = data.Scale.InitialValue;
            var tween = target.DOScale( data.Scale.EndValue, data.Scale.TweenDuration );
            ConfigureTween( tween, data.Scale );
        }

        if ( data.Color.Enabled ) {
            if ( !data.Color.UsePreviousValue ) spriteRenderer.color = data.Color.InitialValue;
            var tween = spriteRenderer.DOColor( data.Color.EndValue, data.Color.TweenDuration );
            ConfigureTween( tween, data.Color );
        }

        if ( endCallback != null ) {
            float t = 0;
            var timerTween = DOTween.To( () => t, ( x ) => t = x, 1f, data.TweenDuration );
            timerTween.OnComplete( endCallback.Invoke );
        }
    }

    protected void ConfigureTween<T> ( Tween tween, TweenData<T> data ) {
        tween.SetEase( data.TweenEase );
        tween.SetDelay( data.TweenDelay );
        if ( data.TweenLoop ) {
            tween.SetLoops( data.TweenLoopCount, data.TweenLoopType );
        }
        _activeTweens.Add( tween );
    }

    protected virtual void ResetTarget () {
        spriteRenderer.color = _initialColor;
        target.localPosition = _initialLocalPosition;
        target.localScale = _initialLocalScale;
    }
}