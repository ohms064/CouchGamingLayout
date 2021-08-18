using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
public class TwitchActiveUser {
    public float Time { get; private set; }

    public string LastMesage { get; private set; }

    public string UserName { get; private set; }

    public string ParentUserId { get; private set; }

    public void UpdateTime ( float time ) {
        Time += time;
    }

    public void UpdateMessage ( string message ) {
        LastMesage = message;
        Time = 0f;
    }

    public void Reset ( string name ) {
        Time = 0;
        LastMesage = "";
        UserName = name;
    }

    public void TalkingTo ( string parentId ) {
        ParentUserId = parentId;
    }
}
