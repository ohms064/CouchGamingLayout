using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Models;
using UnityEngine;

public class CharacterChatMessage {
    private TwitchChatPool _characterPool;
    private EmoteSet _emotes;
    public string Message { get; private set; }
    public string UserName { get; private set; }
    public string UserId { get; private set; }
    public CharacterChatMessage Parent { get; private set; }
    public List<Emote> Emotes => _emotes.Emotes;
    public bool Responding => Parent != null;
    public CharacterHandler UserCharacter => _characterPool[UserId];
    public CharacterHandler ParentCharacter => Responding ? _characterPool[Parent.UserId] : null;
    public TwitchChatPool Chatters => _characterPool;
    public bool HasEmotes => Emotes != null;

    public CharacterChatMessage ( string message, string userName, string userId, TwitchChatPool pool, EmoteSet emotes, CharacterChatMessage parent ) {
        Message = message;
        UserName = userName;
        UserId = userId;
        Parent = parent;
        _emotes = emotes;
        _characterPool = pool;
    }
}
