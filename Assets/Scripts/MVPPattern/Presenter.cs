using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField, Tooltip("Model")]
    Model _model = default;

    [SerializeField, Tooltip("View")]
    View _view = default;


}
