using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using Sirenix.OdinInspector;

public class TwitchApiConnection : MonoBehaviour {
    [SerializeField, Required]
    private TwitchCredentials _credentials;

    public Api TwitchApi { get; private set; }

    // Start is called before the first frame update
    void Awake () {

        TwitchApi = new Api();
        TwitchApi.Settings.AccessToken = _credentials.botAccesToken;
        TwitchApi.Settings.ClientId = _credentials.clientId;
    }
}
