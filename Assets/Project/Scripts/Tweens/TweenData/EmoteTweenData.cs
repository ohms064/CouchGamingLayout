using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu (menuName = "TweenData/Emote")]
public class EmoteTweenData : DinoTweenData {
    public Sprite EmoteSprite { get => _emoteSprite; }
    public float SpriteHideTime { get => _spriteHideTime; }

    [SerializeField, PreviewField, AssetSelector (Paths = "Assets/Kenney/Emote Pack/PNG/Pixel/Style 1"), Title( "Sprite" )]
    private Sprite _emoteSprite;
    [SerializeField, MinValue( 0f )]
    private float _spriteHideTime = 0.5f;
}