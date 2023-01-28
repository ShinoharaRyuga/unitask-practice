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

    #region Action
    /// <summary>UIを初期化する処理 </summary>
    event Action<float, float> _onInitializeUI = default;
    /// <summary>HPUIを更新する処理 </summary>
    event Action<float, float, float> _onHpUIUpdate = default;
    /// <summary>MPUIを更新する処理 </summary>
    event Action<float, float, float> _onMpUIUpdate = default;
    /// <summary>コマンドの実行結果を表示するテキストを更新する処理 </summary>
    event Action<Command> _onCommandResultTextUpdate = default;

    /// <summary>UIを初期化する処理 </summary>
    public event Action<float, float> OnInitializeUI
    {
        add { _onInitializeUI += value; }
        remove { _onInitializeUI -= value; }
    }

    /// <summary>HPUIを更新する処理 </summary>
    public event Action<float, float, float> OnHpUIUpdate
    {
        add { _onHpUIUpdate += value; }
        remove { _onHpUIUpdate -= value; }
    }

    /// <summary>MPUIを更新する処理 </summary>
    public event Action<float, float, float> OnMpUIUpdate
    {
        add { _onMpUIUpdate += value; }
        remove { _onMpUIUpdate -= value; }
    }

    /// <summary>コマンドの実行結果を表示するテキストを更新する処理 </summary>
    public event Action<Command> OnCommandResultTextUpdate
    {
        add { _onCommandResultTextUpdate += value; }
        remove { _onCommandResultTextUpdate -= value; }
    }
    #endregion

    public void Start()
    {
        Initialize();
    }

    /// <summary>初期化処理 </summary>
    void Initialize()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
        _onInitializeUI?.Invoke(_currentHp, _currentMp);
    }

    /// <summary>ダメージを受ける </summary>
    /// <param name="damageValue">ダメージ量</param>
    public void GetDamage(float damageValue)
    {
        var nextHp = Mathf.Max(_currentHp - damageValue, 0);
        _onHpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _onCommandResultTextUpdate?.Invoke(Command.Damage);
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

        _onHpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _onMpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);
        _onCommandResultTextUpdate?.Invoke(Command.Heal);

        _currentHp = nextHp;
        _currentMp = nextMp;
    }

    /// <summary>Mpを回復する</summary>
    /// <param name="recoverValue">回復量</param>
    public void RecoverMp(float recoverValue)
    {
        if (_maxMp <= _currentMp) { return; }

        var nextMp = Mathf.Min(_currentMp + recoverValue, _maxMp);
        _onMpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);
        _onCommandResultTextUpdate?.Invoke(Command.RecoverMp);

        _currentMp = nextMp;
    }
}

public enum Command
{
    Damage,
    Heal,
    RecoverMp,
}
