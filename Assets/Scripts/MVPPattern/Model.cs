using System;
using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�Hp")]
    float _maxHp = 100;
    [SerializeField, Tooltip("�ő�Mp")]
    float _maxMp = 100;
    [SerializeField, Tooltip("Hp�񕜎��̏���MP")]
    float _useMp = 25;

    /// <summary>���݂�Hp </summary>
    float _currentHp = 0;
    /// <summary>���݂�Mp </summary>
    float _currentMp = 0;

    #region Action
    /// <summary>UI�����������鏈�� </summary>
    event Action<float, float> _onInitializeUI = default;
    /// <summary>HPUI���X�V���鏈�� </summary>
    event Action<float, float, float> _onHpUIUpdate = default;
    /// <summary>MPUI���X�V���鏈�� </summary>
    event Action<float, float, float> _onMpUIUpdate = default;
    /// <summary>�R�}���h�̎��s���ʂ�\������e�L�X�g���X�V���鏈�� </summary>
    event Action<Command> _onCommandResultTextUpdate = default;

    /// <summary>UI�����������鏈�� </summary>
    public event Action<float, float> OnInitializeUI
    {
        add { _onInitializeUI += value; }
        remove { _onInitializeUI -= value; }
    }

    /// <summary>HPUI���X�V���鏈�� </summary>
    public event Action<float, float, float> OnHpUIUpdate
    {
        add { _onHpUIUpdate += value; }
        remove { _onHpUIUpdate -= value; }
    }

    /// <summary>MPUI���X�V���鏈�� </summary>
    public event Action<float, float, float> OnMpUIUpdate
    {
        add { _onMpUIUpdate += value; }
        remove { _onMpUIUpdate -= value; }
    }

    /// <summary>�R�}���h�̎��s���ʂ�\������e�L�X�g���X�V���鏈�� </summary>
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

    /// <summary>���������� </summary>
    void Initialize()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
        _onInitializeUI?.Invoke(_currentHp, _currentMp);
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damageValue">�_���[�W��</param>
    public void GetDamage(float damageValue)
    {
        var nextHp = Mathf.Max(_currentHp - damageValue, 0);
        _onHpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _onCommandResultTextUpdate?.Invoke(Command.Damage);
        _currentHp = nextHp;

        if (_currentHp <= 0)
        {
            Debug.Log("��");
        }
    }

    /// <summary>Hp���񕜂��� </summary>
    /// <param name="healValue">�񕜗�</param>
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

    /// <summary>Mp���񕜂���</summary>
    /// <param name="recoverValue">�񕜗�</param>
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
