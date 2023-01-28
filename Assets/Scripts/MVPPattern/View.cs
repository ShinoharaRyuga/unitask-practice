using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class View : MonoBehaviour
{
    readonly Vector2 COMMAND_TEXT_DEFAULT_POSITION = new Vector2(-250f, -380f);

    readonly Vector2 COMMAND_TEXT_ANIMATION_POSITION = new Vector2(-250f, -300f);

    [SerializeField, Tooltip("�A�j���[�V�����ɂ����鎞��")]
    float _interval = default;
    [SerializeField, Tooltip("HP�Q�[�W")]
    Slider _hpGage = default;
    [SerializeField, Tooltip("MP�Q�[�W")]
    Slider _mpGage = default;
    [SerializeField, Tooltip("�R�}���h��\������e�L�X�g")]
    Text _commandText = default;

    /// <summary>���݂�Hp��\������e�L�X�g</summary>
    Text _currentHpValueText => _hpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>���݂�Mp��\������e�L�X�g</summary>
    Text _currentMpValueText => _mpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>�ő�Hp��\������e�L�X�g </summary>
    Text maxHpText => _hpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>�ő�Mp��\������e�L�X�g </summary>
    Text maxMpText => _mpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>�p�����[�^�e�L�X�g�𑀍삷�邽�߂ɕK�v�Ȍ������擾���邽�߂̊֐� </summary>
    /// <param name="parameter">�p�����[�^�̒l</param>
    /// <returns>����</returns>
    string GetParameterDigit(int parameter)
    {
        if(parameter.ToString().Length - 1 == 0)
        {
            return "0";
        }

        var format = "";

        for (var i = 0; i < parameter.ToString().Length - 1; i++)
        {
            format += "0";
        }

        return format;
    }

    /// <summary> HpUI�̍X�V�������s�� </summary>
    /// <param name="beforeHp">�X�V�O��Hp</param>
    /// <param name="afterHp">�X�V���Hp</param>
    public void HpChangeValue(float value, float beforeHp, float afterHp)
    {
        var beforeHpFormat = GetParameterDigit((int)beforeHp);
        var afterHpFormat = GetParameterDigit((int)afterHp);
       
        DOTween.To(() => _hpGage.value, x => _hpGage.value = x, value, _interval);      //�Q�[�W����

        DOTween.To(() => beforeHp, x => beforeHp = x, afterHp, _interval)               //�e�L�X�g�𑀍�
                 .OnUpdate(() => _currentHpValueText.text = beforeHp.ToString(beforeHpFormat))
                 .OnComplete(() => _currentHpValueText.text = afterHp.ToString(afterHpFormat));
    }

    /// <summary> MpUI�̍X�V�������s�� </summary>
    /// <param name="beforeMp">�X�V�O��Mp</param>
    /// <param name="afterMp">�X�V�O��Mp</param>
    public void MpChangeValue(float value, float beforeMp, float afterMp)
    {
        var beforeMpFormat = GetParameterDigit((int)beforeMp);
        var afterMpFormat = GetParameterDigit((int)afterMp);

        DOTween.To(() => _mpGage.value, x => _mpGage.value = x, value, _interval);  //�Q�[�W����

        DOTween.To(() => beforeMp, x => beforeMp = x, afterMp, _interval)           //�e�L�X�g�𑀍�
                .OnUpdate(() => _currentMpValueText.text = beforeMp.ToString(beforeMpFormat))
                .OnComplete(() => _currentMpValueText.text = afterMp.ToString(afterMpFormat));
    }

    /// <summary>���������� </summary>
    public void Initialize(float maxHp, float maxMp)
    {
        maxHpText.text = maxHp.ToString();
        maxMpText.text = maxMp.ToString();
        _currentHpValueText.text = maxHp.ToString();
        _currentMpValueText.text = maxMp.ToString();
    }

    /// <summary>�R�}���h�̎��s���ʂ��e�L�X�g�ɕ\������ </summary>
    public void ShowCommandText(Command command)
    {
        _commandText.rectTransform.localPosition = COMMAND_TEXT_DEFAULT_POSITION;
        _commandText.color = new Color(_commandText.color.r, _commandText.color.g, _commandText.color.b, 1f);

        var sequence = DOTween.Sequence();

        switch (command)
        {
            case Command.Damage:
                _commandText.text = "�_���[�W�������I";
                break;
            case Command.Heal:
                _commandText.text = "HP���񕜂���!";
                break;
            case Command.RecoverMp:
                _commandText.text = "MP���񕜂���!";
                break;
        }

        sequence.Append(_commandText.rectTransform.DOAnchorPos(COMMAND_TEXT_ANIMATION_POSITION, _interval))
            .Append(_commandText.DOFade(0f, _interval));
    }
}
