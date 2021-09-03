using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TwitchActiveUser : MonoBehaviour {

    public float Time { get; private set; }

    [ShowInInspector]
    public CharacterChatMessage LastMessage { get; private set; }

    public void UpdateTime ( float time ) {
        Time += time;
    }

    public void OnSpawn ( CharacterChatMessage message ) {
        LastMessage = message;
        Time = 0;
    }
}
