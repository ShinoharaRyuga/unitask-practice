using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class View : MonoBehaviour
{
    [SerializeField, Tooltip("�A�j���[�V�����ɂ����鎞��")]
    float _interval = default;
    [SerializeField]
    Slider _hpGage = default;
    [SerializeField]
    Slider _mpGage = default;

    /// <summary>���݂�Hp��\������e�L�X�g</summary>
    Text _currentHpValueText => _hpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>���݂�Mp��\������e�L�X�g</summary>
    Text _currentMpValueText => _mpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>�ő�Hp��\������e�L�X�g </summary>
    Text maxHpText => _hpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>�ő�Mp��\������e�L�X�g </summary>
    Text maxMpText => _mpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary> HpUI�̍X�V�������s�� </summary>
    /// <param name="beforeHp">�X�V�O��Hp</param>
    /// <param name="afterHp">�X�V���Hp</param>
    public void HpChangeValue(float value, float beforeHp, float afterHp)
    {
        DOTween.To(() => _hpGage.value, x => _hpGage.value = x, value, _interval);      //�Q�[�W����

        DOTween.To(() => beforeHp, x => beforeHp = x, afterHp, _interval)               //�e�L�X�g�𑀍�
                 .OnUpdate(() => _currentHpValueText.text = beforeHp.ToString("000"))
                 .OnComplete(() => _currentHpValueText.text = afterHp.ToString("000"));
    }

    /// <summary> MpUI�̍X�V�������s�� </summary>
    /// <param name="beforeMp">�X�V�O��Mp</param>
    /// <param name="afterMp">�X�V�O��Mp</param>
    public void MpChangeValue(float value, float beforeMp, float afterMp)
    {
        DOTween.To(() => _mpGage.value, x => _mpGage.value = x, value, _interval);  //�Q�[�W����

        DOTween.To(() => beforeMp, x => beforeMp = x, afterMp, _interval)           //�e�L�X�g�𑀍�
                .OnUpdate(() => _currentMpValueText.text = beforeMp.ToString("00"))
                .OnComplete(() => _currentMpValueText.text = afterMp.ToString("00"));
    }

    /// <summary>���������� </summary>
    public void Initialize(float maxHp, float maxMp)
    {
        maxHpText.text = maxHp.ToString();
        maxMpText.text = maxMp.ToString();
    }
}