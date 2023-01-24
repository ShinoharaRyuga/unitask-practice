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

    /// <summary>ダメージを受ける </summary>
    /// <param name="damageValue">ダメージ量</param>
    public void GetDamage(float damageValue)
    {
        _currentHp -= damageValue;
        _currentMp -= _useMp;

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

        _currentHp = Mathf.Min(_currentHp + healValue, _maxHp);
    }

    /// <summary>Mpを回復する</summary>
    /// <param name="recoverValue">回復量</param>
    public void RecoverMp(float recoverValue)
    {
        if(_maxMp <= _currentMp) { return; }

        _currentMp = Mathf.Min(_currentMp + recoverValue, _maxMp);
    }
}
