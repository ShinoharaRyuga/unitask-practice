using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class View : MonoBehaviour
{
    readonly Vector2 COMMAND_TEXT_DEFAULT_POSITION = new Vector2(-250f, -380f);

    readonly Vector2 COMMAND_TEXT_ANIMATION_POSITION = new Vector2(-250f, -300f);

    [SerializeField, Tooltip("アニメーションにかかる時間")]
    float _interval = default;
    [SerializeField, Tooltip("HPゲージ")]
    Slider _hpGage = default;
    [SerializeField, Tooltip("MPゲージ")]
    Slider _mpGage = default;
    [SerializeField, Tooltip("コマンドを表示するテキスト")]
    Text _commandText = default;

    /// <summary>現在のHpを表示するテキスト</summary>
    Text _currentHpValueText => _hpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>現在のMpを表示するテキスト</summary>
    Text _currentMpValueText => _mpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>最大Hpを表示するテキスト </summary>
    Text maxHpText => _hpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>最大Mpを表示するテキスト </summary>
    Text maxMpText => _mpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>パラメータテキストを操作するために必要な桁数を取得するための関数 </summary>
    /// <param name="parameter">パラメータの値</param>
    /// <returns>桁数</returns>
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

    /// <summary> HpUIの更新処理を行う </summary>
    /// <param name="beforeHp">更新前のHp</param>
    /// <param name="afterHp">更新後のHp</param>
    public void HpChangeValue(float value, float beforeHp, float afterHp)
    {
        var beforeHpFormat = GetParameterDigit((int)beforeHp);
        var afterHpFormat = GetParameterDigit((int)afterHp);
       
        DOTween.To(() => _hpGage.value, x => _hpGage.value = x, value, _interval);      //ゲージ操作

        DOTween.To(() => beforeHp, x => beforeHp = x, afterHp, _interval)               //テキストを操作
                 .OnUpdate(() => _currentHpValueText.text = beforeHp.ToString(beforeHpFormat))
                 .OnComplete(() => _currentHpValueText.text = afterHp.ToString(afterHpFormat));
    }

    /// <summary> MpUIの更新処理を行う </summary>
    /// <param name="beforeMp">更新前のMp</param>
    /// <param name="afterMp">更新前のMp</param>
    public void MpChangeValue(float value, float beforeMp, float afterMp)
    {
        var beforeMpFormat = GetParameterDigit((int)beforeMp);
        var afterMpFormat = GetParameterDigit((int)afterMp);

        DOTween.To(() => _mpGage.value, x => _mpGage.value = x, value, _interval);  //ゲージ操作

        DOTween.To(() => beforeMp, x => beforeMp = x, afterMp, _interval)           //テキストを操作
                .OnUpdate(() => _currentMpValueText.text = beforeMp.ToString(beforeMpFormat))
                .OnComplete(() => _currentMpValueText.text = afterMp.ToString(afterMpFormat));
    }

    /// <summary>初期化処理 </summary>
    public void Initialize(float maxHp, float maxMp)
    {
        maxHpText.text = maxHp.ToString();
        maxMpText.text = maxMp.ToString();
        _currentHpValueText.text = maxHp.ToString();
        _currentMpValueText.text = maxMp.ToString();
    }

    /// <summary>コマンドの実行結果をテキストに表示する </summary>
    public void ShowCommandText(Command command)
    {
        _commandText.rectTransform.localPosition = COMMAND_TEXT_DEFAULT_POSITION;
        _commandText.color = new Color(_commandText.color.r, _commandText.color.g, _commandText.color.b, 1f);

        var sequence = DOTween.Sequence();

        switch (command)
        {
            case Command.Damage:
                _commandText.text = "ダメージうけた！";
                break;
            case Command.Heal:
                _commandText.text = "HPが回復した!";
                break;
            case Command.RecoverMp:
                _commandText.text = "MPが回復した!";
                break;
        }

        sequence.Append(_commandText.rectTransform.DOAnchorPos(COMMAND_TEXT_ANIMATION_POSITION, _interval))
            .Append(_commandText.DOFade(0f, _interval));
    }
}
