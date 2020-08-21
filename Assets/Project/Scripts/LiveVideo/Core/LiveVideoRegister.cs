using UnityEngine;
using UnityEngine.Events;

public class LiveVideoRegister : MonoBehaviour {
    public string TokenId { get; set; }
    public string VideoId { get; set; }

    [SerializeField]
    private UnityEvent _onValid;

    public void Register () {
        LiveVideoWrapper.SubscribeToComments (VideoId, TokenId);
        LiveVideoWrapper.SubscribeToReactions (VideoId, TokenId);
    }

    public void CheckValid () {
        if (string.IsNullOrWhiteSpace (VideoId)) return;
        _onValid.Invoke ();
    }
}