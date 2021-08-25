using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ChatPool", menuName = "Twitch/Chat pool" )]
public class TwitchChatPool : ScriptableObject, IDictionary<string, DinoCharacter> {
    [ShowInInspector, HideInEditorMode]
    private Dictionary<string, DinoCharacter> _chatUsers = new Dictionary<string, DinoCharacter>();

    public DinoCharacter this[string key] { get => _chatUsers[key]; set => _chatUsers[key] = value; }

    public ICollection<string> Keys => _chatUsers.Keys;

    public ICollection<DinoCharacter> Values => _chatUsers.Values;

    public int Count => _chatUsers.Count;

    public bool IsReadOnly => false;
    public void Add ( KeyValuePair<string, DinoCharacter> item ) {
        Add( item.Key, item.Value );
    }

    public void Add ( string key, DinoCharacter value ) {
        _chatUsers.Add( key, value );
    }

    public void Clear () {
        _chatUsers.Clear();
    }

    public bool Contains ( KeyValuePair<string, DinoCharacter> item ) {
        return _chatUsers.ContainsKey( item.Key ) && _chatUsers.ContainsValue( item.Value );
    }

    public bool ContainsKey ( string key ) {
        return _chatUsers.ContainsKey( key );
    }

    public void CopyTo ( KeyValuePair<string, DinoCharacter>[] array, int arrayIndex ) {

    }

    public IEnumerator<KeyValuePair<string, DinoCharacter>> GetEnumerator () {
        foreach ( var pair in _chatUsers ) {
            yield return pair;
        }
    }

    public bool Remove ( string key ) {
        return _chatUsers.Remove( key );
    }

    public bool Remove ( KeyValuePair<string, DinoCharacter> item ) {
        if ( !_chatUsers.ContainsValue( item.Value ) ) return false;
        return _chatUsers.Remove( item.Key );
    }

    public bool TryGetValue ( string key, out DinoCharacter value ) {
        return _chatUsers.TryGetValue( key, out value );
    }

    IEnumerator IEnumerable.GetEnumerator () {
        foreach ( var pair in _chatUsers ) {
            yield return pair;
        }
    }
}
