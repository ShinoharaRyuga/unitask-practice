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

    /// <summary>UI�����������鏈�� </summary>
    event Action<float, float> _initializeUI = default;
    /// <summary>HPUI���X�V���鏈�� </summary>
    event Action<float, float, float> _hpUIUpdate = default;
    /// <summary>MPUI���X�V���鏈�� </summary>
    event Action<float, float, float> _mpUIUpdate = default;

    /// <summary>UI�����������鏈�� </summary>
    public event Action<float, float> InitializeUI
    {
        add { _initializeUI += value; }
        remove { _initializeUI -= value; }
    }

    /// <summary>HPUI���X�V���鏈�� </summary>
    public event Action<float, float, float> HpUIUpdate
    {
        add { _hpUIUpdate += value; }
        remove { _hpUIUpdate -= value; }
    }

    /// <summary>MPUI���X�V���鏈�� </summary>
    public event Action<float, float, float> MpUIUpdate
    {
        add { _mpUIUpdate += value; }
        remove { _mpUIUpdate -= value; }
    }

    public void Start()
    {
        Initialize();
    }

    /// <summary>���������� </summary>
    void Initialize()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
        _initializeUI?.Invoke(_currentHp, _currentMp);
    }

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damageValue">�_���[�W��</param>
    public void GetDamage(float damageValue)
    {
        var nextHp = Mathf.Max(_currentHp - damageValue, 0);
        _hpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
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

        _hpUIUpdate?.Invoke(nextHp / _maxHp, _currentHp, nextHp);
        _mpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);

        _currentHp = nextHp;
        _currentMp = nextMp;
    }

    /// <summary>Mp���񕜂���</summary>
    /// <param name="recoverValue">�񕜗�</param>
    public void RecoverMp(float recoverValue)
    {
        if(_maxMp <= _currentMp) { return; }

        var nextMp = Mathf.Min(_currentMp + recoverValue, _maxMp);
        _mpUIUpdate?.Invoke(nextMp / _maxMp, _currentMp, nextMp);

        _currentMp = nextMp;
    }
}
