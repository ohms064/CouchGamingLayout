using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu (menuName = "TweenData/Dino")]
public class DinoTweenData : ScriptableObject {
    public Vector3TweenData Movement { get => _movement; }
    public Vector3TweenData Scale { get => _scale; }
    public ColorTweenData Color { get => _color; }

    [SerializeField, Title ("Movement"), HideLabel]
    private Vector3TweenData _movement = new Vector3TweenData ();

    [SerializeField, Title ("Scale"), HideLabel]
    private Vector3TweenData _scale = new Vector3TweenData ();

    [SerializeField, Title( "Color" ), HideLabel]
    private ColorTweenData _color = new ColorTweenData();
}