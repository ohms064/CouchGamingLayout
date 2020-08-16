using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
public class TweenData<T> {

    public bool Enabled { get => _enabled; }
    public Ease TweenEase { get => _ease; }
    public float TweenDuration { get => _duration; }
    public float TweenDelay { get => _delay; }

    public bool TweenLoop { get => _loop; }
    public LoopType TweenLoopType { get => _loopType; }
    public int TweenLoopCount { get => _loopCount; }
    public bool UsePreviousValue { get => _usePreviousValue; }
    public T InitialValue { get => _initialValue; }
    public T EndValue { get => _endValue; }

    [SerializeField]
    private bool _enabled = true;
    [SerializeField, EnableIf( "_enabled" )]
    private Ease _ease = Ease.Linear;
    [SerializeField, MinValue( 0f ), EnableIf( "_enabled" )]
    private float _duration = 0.4f;
    [SerializeField, MinValue( 0f ), EnableIf( "_enabled" )]
    private float _delay;

    [SerializeField, EnableIf( "_enabled" )]
    private bool _loop;
    [SerializeField, EnableIf( "_enabled" ), ShowIf( "_loop" )]
    private LoopType _loopType;
    [SerializeField, MinValue( -1 ), EnableIf( "_enabled" ), ShowIf( "_loop" )]
    private int _loopCount;

    [SerializeField, EnableIf( "_enabled" )]
    private bool _usePreviousValue;
    [SerializeField, EnableIf( "_enabled" ), DisableIf( "_usePreviousValue" )]
    private T _initialValue;
    [SerializeField, EnableIf( "_enabled" )]
    private T _endValue;
}

[System.Serializable]
public class FloatTweenData : TweenData<float> { }

[System.Serializable]
public class Vector3TweenData : TweenData<Vector3> { }

[System.Serializable]
public class ColorTweenData : TweenData<Color> { }