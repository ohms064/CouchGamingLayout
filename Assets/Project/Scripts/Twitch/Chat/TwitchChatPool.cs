using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ChatPool", menuName = "Twitch/Chat pool" )]
public class TwitchChatPool : ScriptableObject, IDictionary<string, CharacterHandler> {
    [ShowInInspector, HideInEditorMode]
    private Dictionary<string, CharacterHandler> _chatUsers = new Dictionary<string, CharacterHandler>();

    private bool _updated = false;
    private event System.Action<TwitchChatPool> _onUpdated;

    public CharacterHandler this[string key] { get => _chatUsers[key]; set => _chatUsers[key] = value; }
    public ICollection<string> Keys => _chatUsers.Keys;
    public ICollection<CharacterHandler> Values => _chatUsers.Values;
    public int Count => _chatUsers.Count;
    public bool Updated { get => _updated; set { _updated = true; _onUpdated?.Invoke( this ); } }
    public bool IsReadOnly => false;

    public void RegisterOnUpdated(System.Action<TwitchChatPool> onUpdated ) {
        _onUpdated += onUpdated;
    }

    public void UnregisterOnUpdated ( System.Action<TwitchChatPool> onUpdated ) {
        _onUpdated -= onUpdated;
    }

    public void Add ( KeyValuePair<string, CharacterHandler> item ) {
        Add( item.Key, item.Value );
    }

    public void Add ( string key, CharacterHandler value ) {
        _chatUsers.Add( key, value );
    }

    public void Clear () {
        _chatUsers.Clear();
    }

    public bool Contains ( KeyValuePair<string, CharacterHandler> item ) {
        return _chatUsers.ContainsKey( item.Key ) && _chatUsers.ContainsValue( item.Value );
    }

    public bool ContainsKey ( string key ) {
        return _chatUsers.ContainsKey( key );
    }

    public void CopyTo ( KeyValuePair<string, CharacterHandler>[] array, int arrayIndex ) {

    }

    public IEnumerator<KeyValuePair<string, CharacterHandler>> GetEnumerator () {
        foreach ( var pair in _chatUsers ) {
            yield return pair;
        }
    }

    public bool Remove ( string key ) {
        return _chatUsers.Remove( key );
    }

    public bool Remove ( KeyValuePair<string, CharacterHandler> item ) {
        if ( !_chatUsers.ContainsValue( item.Value ) ) return false;
        return _chatUsers.Remove( item.Key );
    }

    public bool TryGetValue ( string key, out CharacterHandler value ) {
        return _chatUsers.TryGetValue( key, out value );
    }

    IEnumerator IEnumerable.GetEnumerator () {
        foreach ( var pair in _chatUsers ) {
            yield return pair;
        }
    }
}
