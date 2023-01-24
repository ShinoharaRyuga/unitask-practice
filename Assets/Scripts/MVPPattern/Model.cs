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

    /// <summary>�_���[�W���󂯂� </summary>
    /// <param name="damageValue">�_���[�W��</param>
    public void GetDamage(float damageValue)
    {
        _currentHp -= damageValue;
        _currentMp -= _useMp;

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

        _currentHp = Mathf.Min(_currentHp + healValue, _maxHp);
    }

    /// <summary>Mp���񕜂���</summary>
    /// <param name="recoverValue">�񕜗�</param>
    public void RecoverMp(float recoverValue)
    {
        if(_maxMp <= _currentMp) { return; }

        _currentMp = Mathf.Min(_currentMp + recoverValue, _maxMp);
    }
}
