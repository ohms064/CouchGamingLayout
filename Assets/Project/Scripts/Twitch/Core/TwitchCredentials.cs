using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Credentials", menuName = "Twitch/Credentials" )]
public class TwitchCredentials : ScriptableObject {
    [Required]
    public string channelName;

    [Required, BoxGroup( "Client" )]
    public string clientId;
    [Required, BoxGroup( "Client" )]
    public string clientSecret;

    [Required, BoxGroup( "Bot" )]
    public string botName;
    [Required, BoxGroup( "Bot" )]
    public string botAccesToken;
    [Required, BoxGroup( "Bot" )]
    public string botRefreshToken;
    [Required, BoxGroup( "Bot" )]
    public string botClientId;
}
