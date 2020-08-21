using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OhmsLibraries.Utilities.Events;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent( typeof( DinoTween ) )]
public class TweenManager : SerializedMonoBehaviour {
    [SerializeField]
    private Dictionary<string, DinoTweenData> _emotes = new Dictionary<string, DinoTweenData>();
    [SerializeField]
    private UnityStringEvent _onEmote, _onEmoteEnd;
    private DinoTween _tweens;

    private void Awake () {
        _tweens = GetComponent<DinoTween>();
    }

    [Button, HideInEditorMode]
    public void SetEmotion ( string emotion ) {
        _onEmote.Invoke( emotion );
        if ( !_emotes.ContainsKey( emotion ) ) return;
        _tweens.Appear( _emotes[emotion], () => _onEmoteEnd.Invoke( emotion ) );
    }

}