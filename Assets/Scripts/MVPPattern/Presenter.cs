using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField, Tooltip("Model")]
    Model _model = default;

    [SerializeField, Tooltip("View")]
    View _view = default;

    private void Awake()
    {
        _model.InitializeUI += _view.Initialize;
        _model.HpUIUpdate += _view.HpChangeValue;
        _model.MpUIUpdate += _view.MpChangeValue;
    }
}
