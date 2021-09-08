using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpriteAnimation {
    [SerializeField, PreviewField]
    private Sprite[] _sprites;
    [SerializeField]
    private float _changeRate = 0.1f;

    public Sprite[] Sprites => _sprites;
    public float ChangeRate => _changeRate;

}


