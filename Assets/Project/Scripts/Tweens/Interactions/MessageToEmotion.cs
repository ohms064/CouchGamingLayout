using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Text.RegularExpressions;

public class MessageToEmotion : MonoBehaviour {
    [SerializeField, Required, InlineEditor]
    private TweenManager _manager;
    [SerializeField]
    private EmoteMessage[] _emotions;
    [SerializeField]
    private int _checksPerFrame = 1;

    private Queue<System.Func<string>> _checkQueue;

    public void CheckMessage ( string message ) {
        if ( !enabled ) enabled = true;
        foreach ( var emotion in _emotions ) {
            _checkQueue.Enqueue( () => emotion.CheckMessage( message ) );
        }
    }

    private void Awake () {
        _checkQueue = new Queue<System.Func<string>>();
        foreach ( var emotion in _emotions ) {
            emotion.BuildRegex();
        }
    }

    private void Update () {
        if ( _checkQueue.Count == 0 ) {
            enabled = false;
            return;
        }
        for ( int i = 0; i < _checksPerFrame; i++ ) {
            var result = _checkQueue.Dequeue().Invoke();
            if ( !string.IsNullOrEmpty( result ) ) {
                _manager.SetEmotion( result );
            }
        }
    }
}

[System.Serializable]
public class EmoteMessage {
    [SerializeField]
    private string _regexPattern;
    [SerializeField]
    private string _animation;

    private Regex _regex;

    public void BuildRegex () {
        try {
            _regex = new Regex( _regexPattern, RegexOptions.IgnoreCase );
        }
        catch ( System.Exception e ) {
            Debug.LogError( $"Error in {_animation}: {e.Message}" );
        }
    }

    public string CheckMessage ( string message ) {
        var match = _regex.Match( message );
        return match.Success ? _animation : null;
    }
}

