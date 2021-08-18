using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;

public class TwitchViewers : MonoBehaviour {
    [SerializeField]
    private TwitchApiConnection _api;
    [SerializeField]
    private TwitchClientConnection _client;

    private void Start () {
        _api.TwitchApi.Invoke( _api.TwitchApi.Undocumented.GetChattersAsync( _client.TwitchClient.JoinedChannels[0].Channel ), OnChatterList );
    }

    private void OnChatterList ( List<ChatterFormatted> chatters ) {
        foreach ( var viewer in chatters ) {

        }

    }
}
