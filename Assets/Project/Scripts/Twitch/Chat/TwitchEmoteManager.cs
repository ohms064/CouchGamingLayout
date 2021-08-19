using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class TwitchEmoteManager : MonoBehaviour {
    [SerializeField]
    private int _cacheSize = 20;

    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    public Dictionary<string, Texture2D> _emoteCache = new Dictionary<string, Texture2D>();

    private List<string> _emoteQueue = new List<string>();

    public void GetEmote ( string emoteUrl, System.Action<Texture2D> callback ) {
        if ( _emoteCache.ContainsKey( emoteUrl ) ) {
            callback.Invoke( _emoteCache[emoteUrl] );
            _emoteQueue.Remove( emoteUrl );//Remove to add it on top.
            _emoteQueue.Add( emoteUrl );
        }
        StartCoroutine( DownloadEmote( emoteUrl, callback ) );
    }

    private IEnumerator DownloadEmote ( string url, System.Action<Texture2D> callback ) {
        using ( UnityWebRequest uwr = UnityWebRequestTexture.GetTexture( url ) ) {
            yield return uwr.SendWebRequest();

            if ( uwr.result != UnityWebRequest.Result.Success ) {
                Debug.Log( uwr.error );
                callback.Invoke( null );
            }
            else {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent( uwr );
                AddEmoteToCache( url, texture );
                callback.Invoke( texture );
            }
        }
    }

    private void AddEmoteToCache ( string url, Texture2D texture ) {
        _emoteCache.Add( url, texture );
        if ( _emoteCache.Count > _cacheSize ) {
            var removeUrl = _emoteQueue.First();
            var emote = _emoteCache[removeUrl];
            _emoteCache.Remove( removeUrl );
            Destroy( emote );
        }
    }

    private void OnDestroy () {
        foreach ( var item in _emoteCache ) {
            Destroy( item.Value );
        }
        _emoteCache.Clear();
    }
}
