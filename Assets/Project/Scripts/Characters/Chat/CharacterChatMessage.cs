using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChatMessage {
    public string Message { get; private set; }
    public string UserName { get; private set; }
    public string UserId { get; private set; }
    public CharacterChatMessage Parent { get; private set; }
    public bool Responding => Parent != null;
    private TwitchChatPool _characterPool;
    public CharacterHandler UserCharacter => _characterPool[UserId];
    public CharacterHandler ParentCharacter => Responding ? _characterPool[Parent.UserId] : null;
    public TwitchChatPool Chatters => _characterPool;

    public CharacterChatMessage ( string message, string userName, string userId, TwitchChatPool pool, CharacterChatMessage parent ) {
        Message = message;
        UserName = userName;
        UserId = userId;
        Parent = parent;
        _characterPool = pool;
    }
}
