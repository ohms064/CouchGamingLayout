using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Facebook.Unity;
using UnityEngine;

public class FBLogin : MonoBehaviour {
    [SerializeField]
    private List<string> _permissions = new List<string> { "public_profile", "user_videos" };

    public void Login () {
        if (FB.IsLoggedIn) return;
        FB.LogInWithReadPermissions (_permissions, AuthCallback);
    }

    private void AuthCallback (ILoginResult result) {
        if (FB.IsLoggedIn) {
            // AccessToken class will have session details
            var accesToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log (accesToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in accesToken.Permissions) {
                Debug.Log (perm);
                GameEventMessage.SendEvent ("OnSuccessLogin");
            }
        } else {
            Debug.Log ("User cancelled login");
        }
    }
}