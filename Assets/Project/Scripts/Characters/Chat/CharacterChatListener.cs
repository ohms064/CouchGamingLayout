using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterChatListener : MonoBehaviour
{
    public abstract bool CheckMessage ( CharacterChatMessage message );

    public abstract void ManageMessage ( CharacterChatMessage message );
}
