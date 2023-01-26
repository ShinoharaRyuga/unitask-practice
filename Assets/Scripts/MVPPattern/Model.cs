using System;
using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField, Tooltip("最大Hp")]
    float _maxHp = 100;
    [SerializeField, Tooltip("最大Mp")]
    float _maxMp = 100;
    [SerializeField, Tooltip("Hp回復時の消費MP")]
    float _useMp = 25;

    /// <summary>現在のHp </summary>
    float _currentHp = 0;
    /// <summary>現在のMp </summary>
    float _currentMp = 0;

    /// <summary>UIを初期化する処理 </summary>
    event Action<float, float> _initializeUI = default;
    /// <summary>HPUIを更新する処理 </summary>
    event Action<float, float, float> _hpUIUpdate = default;
    /// <summary>MPUIを更新する処理 </summary>
    event Action<float, float, float> _mpUIUpdate = default;

    /// <summary>UIを初期化する処理 </summary>
    public event Action<float, float> InitializeUI
    {
        add { _initializeUI += value; }
        remove { _initializeUI -= value; }
    }

    /// <summary>HPUIを更新する処理 </summary>
    public event Action<float, float, float> HpUIUpdate
    {
        add { _hpUIUpdate += value; }
        remove { _hpUIUpdate -= value; }
    }

    /// <summary>MPUIを更新する処理 </summary>
    public event Action<float, float, float> MpUIUpdate
    {
        add { _mpUIUpdate += value; }
        remove { _mpUIUpdate -= value; }
    }

    public void Start()
    {
        Initialize();
    }

    /// <summary>初期化処理 </summary>
    void Initialize()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
        _initializeUI?.Invoke(_currentHp, _currentMp);
    }

    /// <summary>ダメージを受ける </summary>
    /// <param name="damageValue">ダメージ量</param>
    public void GetDamage(float damageValue)
    {
        var nextHp = Mathf.Max(_currentHp - damageValue, 0);
        _hpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _currentHp = nextHp;
 
        if (_currentHp <= 0)
        {
            Debug.Log("死");
        }
    }

    /// <summary>Hpを回復する </summary>
    /// <param name="healValue">回復量</param>
    public void HealHp(float healValue)
    {
        if (_maxHp <= _currentHp || _currentMp < _useMp) { return; }

        var nextHp = Mathf.Min(_currentHp + healValue, _maxHp);
        var nextMp = Mathf.Max(_currentMp - _useMp, 0);

        _hpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _mpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);

        _currentHp = nextHp;
        _currentMp = nextMp;
    }

    /// <summary>Mpを回復する</summary>
    /// <param name="recoverValue">回復量</param>
    public void RecoverMp(float recoverValue)
    {
        if(_maxMp <= _currentMp) { return; }

        var nextMp = Mathf.Min(_currentMp + recoverValue, _maxMp);
        _mpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);

        _currentMp = nextMp;
    }
}
