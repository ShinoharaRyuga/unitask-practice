using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class View : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーションにかかる時間")]
    float _interval = default;
    [SerializeField]
    Slider _hpGage = default;
    [SerializeField]
    Slider _mpGage = default;

    /// <summary>現在のHpを表示するテキスト</summary>
    Text _currentHpValueText => _hpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>現在のMpを表示するテキスト</summary>
    Text _currentMpValueText => _mpGage.transform.GetChild(0).GetComponent<Text>();

    /// <summary>最大Hpを表示するテキスト </summary>
    Text maxHpText => _hpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary>最大Mpを表示するテキスト </summary>
    Text maxMpText => _mpGage.transform.GetChild(1).GetComponent<Text>();

    /// <summary> HpUIの更新処理を行う </summary>
    /// <param name="beforeHp">更新前のHp</param>
    /// <param name="afterHp">更新後のHp</param>
    public void HpChangeValue(float value, float beforeHp, float afterHp)
    {
        DOTween.To(() => _hpGage.value, x => _hpGage.value = x, value, _interval);      //ゲージ操作

        DOTween.To(() => beforeHp, x => beforeHp = x, afterHp, _interval)               //テキストを操作
                 .OnUpdate(() => _currentHpValueText.text = beforeHp.ToString("000"))
                 .OnComplete(() => _currentHpValueText.text = afterHp.ToString("000"));
    }

    /// <summary> MpUIの更新処理を行う </summary>
    /// <param name="beforeMp">更新前のMp</param>
    /// <param name="afterMp">更新前のMp</param>
    public void MpChangeValue(float value, float beforeMp, float afterMp)
    {
        DOTween.To(() => _mpGage.value, x => _mpGage.value = x, value, _interval);  //ゲージ操作

        DOTween.To(() => beforeMp, x => beforeMp = x, afterMp, _interval)           //テキストを操作
                .OnUpdate(() => _currentMpValueText.text = beforeMp.ToString("00"))
                .OnComplete(() => _currentMpValueText.text = afterMp.ToString("00"));
    }

    /// <summary>初期化処理 </summary>
    public void Initialize(float maxHp, float maxMp)
    {
        maxHpText.text = maxHp.ToString();
        maxMpText.text = maxMp.ToString();
    }
}
