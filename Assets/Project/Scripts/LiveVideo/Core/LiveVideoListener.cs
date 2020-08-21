using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LiveVideoListener : MonoBehaviour {
    private void Awake () {
        name = "FB_Connection";
    }

    public void OnUserComment (string jsonData) {
        Debug.Log (jsonData);
    }

    public void OnUserReaction (string jsonData) {
        Debug.Log (jsonData);
    }

#if UNITY_EDITOR
    private void Reset () {
        name = "FB_Connection";
    }
#endif
}