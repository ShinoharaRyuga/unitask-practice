using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField, Tooltip("Model")]
    Model _model = default;

    [SerializeField, Tooltip("View")]
    View _view = default;

    private void Awake()
    {
        _model.OnInitializeUI += _view.Initialize;
        _model.OnHpUIUpdate += _view.HpChangeValue;
        _model.OnMpUIUpdate += _view.MpChangeValue;
        _model.OnCommandResultTextUpdate += _view.ShowCommandText;
    }
}
