using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LivesCount : MonoBehaviour {
    [SerializeField]
    private int _lives;
    [SerializeField]
    private TextMeshProUGUI _tmp;
    [SerializeField]
    private string _format = "{0}";

    public void ReduceLives () {
        _lives--;
        _tmp.text = string.Format( _format, _lives );
    }

    public void AddeLives () {
        _lives++;
        _tmp.text = string.Format( _format, _lives );
    }
}