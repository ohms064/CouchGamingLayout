using Doozy.Engine;
using UnityEngine;

public class FBLoginManager : MonoBehaviour {
    public void OnSuccessLogin () {
        Debug.Log ("Received succes login event");
        GameEventMessage.SendEvent ("OnSuccessLogin");
    }
}