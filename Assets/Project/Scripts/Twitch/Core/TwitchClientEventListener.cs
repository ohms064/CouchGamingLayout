using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using Sirenix.OdinInspector;

public abstract class TwitchClientEventListener : MonoBehaviour {
    public abstract void Listen ( Client connection );
}
