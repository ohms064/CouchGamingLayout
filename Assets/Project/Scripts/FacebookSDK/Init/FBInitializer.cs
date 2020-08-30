﻿using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FBInitializer : MonoBehaviour {

    // Awake function from Unity's MonoBehavior
    void Awake () {
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init (InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp ();
        }
    }

    private void InitCallback () {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp ();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log ("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown) {
        Debug.Log ($"Unity hide status changed: {isGameShown}");
    }
}