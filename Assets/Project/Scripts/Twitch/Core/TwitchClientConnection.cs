using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using Sirenix.OdinInspector;

public class TwitchClientConnection : MonoBehaviour {
    [SerializeField, Required]
    private TwitchCredentials _credentials;

    [SerializeField]
    private List<TwitchClientEventListener> _listeners;

    public Client TwitchClient { get; private set; }

    // Start is called before the first frame update
    void Awake () {

        var credentials = new ConnectionCredentials( _credentials.botName, _credentials.botAccesToken );
        TwitchClient = new Client();
        TwitchClient.Initialize( credentials, _credentials.channelName );

        foreach ( var listener in _listeners ) {
            listener.Listen( TwitchClient );
        }

        TwitchClient.Connect();
    }
}
