using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Doozy.Engine;
using UnityEngine;

public class LiveVideoWrapper {
#if UNITY_EDITOR
    public static void SubscribeToReactions (string liveVideo, string token) {
        Coroutiner.Start (OnReaction ());
    }

    public static void SubscribeToComments (string liveVideo, string token) {
        Coroutiner.Start (OnComment ());
    }

    private static IEnumerator OnReaction () {
        var receiver = GameObject.Find ("FB_Connection");
        if (!receiver) yield break;
        while (true) {
            yield return new WaitForSeconds (3f);
            receiver.SendMessage ("OnUserReaction", "");
        }
    }

    private static IEnumerator OnComment () {
        var receiver = GameObject.Find ("FB_Connection");
        if (!receiver) yield break;
        while (true) {
            yield return new WaitForSeconds (7f);
            receiver.SendMessage ("OnUserComment", "");
        }
    }
#else    
    [DllImport ("__Internal")]
    public static extern void SubscribeToReactions (string liveVideo, string token);

    [DllImport ("__Internal")]
    public static extern void SubscribeToComments (string liveVideo, string token);
#endif 
}