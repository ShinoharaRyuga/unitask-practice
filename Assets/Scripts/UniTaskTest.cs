using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>Unitaskを使ってみる</summary>
public class UniTaskTest : MonoBehaviour
{
    [SerializeField, Header("遅延時間")]
    float _delayTime = 5;

    CancellationTokenSource _cancellationToken = default;

    void Start()
    {
        _cancellationToken = new CancellationTokenSource();
        DelayTimeAsync(_delayTime, _cancellationToken.Token).Forget();
        WaitUntil(_cancellationToken.Token).Forget();

        //// GameObjectが破棄されるとキャンセル状態にしてくれるtoken
        //CancellationToken token = this.GetCancellationTokenOnDestroy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cancellationToken.Cancel();
            Debug.Log("キャンセル");
        }
    }

    /// <summary>遅延処理 </summary>
    /// <param name="delayTime">待機時間</param>
    /// <param name="token">キャンセルトークン</param>
    async UniTask DelayTimeAsync(float delayTime, CancellationToken token)
    {
        Debug.Log("開始");
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: token);
        Debug.Log($"{delayTime}経過 終了");
    }

    /// <summary>数フレーム待つ </summary>
    async UniTask DelayFrameAsync(int delayFrame, CancellationToken token)
    {
        await UniTask.DelayFrame(delayFrame, cancellationToken: token);

        // Updateのタイミングで1フレーム待機する
        await UniTask.Yield();
    }

    /// <summary>指定した条件がtrueになるまで待機</summary>
    async UniTask WaitUntil(CancellationToken token)
    {
        Debug.Log("spaceキー押せ");
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: token);
        Debug.Log("押された");
    }

    /// <summary>指定した条件がfalseになるまで待機 </summary>
    async UniTask WaitWhile(CancellationToken token)
    {
        var flag = true;
        await UniTask.WaitWhile(() => !flag, cancellationToken:token);
        Debug.Log("falseになりました");
    }
}
