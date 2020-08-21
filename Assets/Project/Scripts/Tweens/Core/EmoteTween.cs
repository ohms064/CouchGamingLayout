using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
public class EmoteTween : DinoTween {

    [SerializeField]
    private DinoTweenData _hideData;

    private bool _requireReset;

    [Button, DisableInEditorMode]
    public override void Appear ( DinoTweenData data, System.Action callback = null ) {
        if ( _requireReset ) {
            ResetTarget();
            _requireReset = false;
        }
        StopAllCoroutines();
        base.Appear( data, callback );
        var emoteTweenData = data as EmoteTweenData;
        if ( emoteTweenData == null ) return;

        spriteRenderer.sprite = emoteTweenData.EmoteSprite;
        StartCoroutine( Hide( emoteTweenData.SpriteHideTime ) );
    }

    private IEnumerator Hide ( float wait ) {
        yield return new WaitForSeconds( wait );
        base.Appear( _hideData );
        _requireReset = true;
    }
}