using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>Unitask���g���Ă݂�</summary>
public class UniTaskTest : MonoBehaviour
{
    [SerializeField, Header("�x������")]
    float _delayTime = 5;

    CancellationTokenSource _cancellationToken = default;

    void Start()
    {
        _cancellationToken = new CancellationTokenSource();
        DelayTimeAsync(_delayTime, _cancellationToken.Token).Forget();
        WaitUntil(_cancellationToken.Token).Forget();

        //// GameObject���j�������ƃL�����Z����Ԃɂ��Ă����token
        //CancellationToken token = this.GetCancellationTokenOnDestroy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cancellationToken.Cancel();
            Debug.Log("�L�����Z��");
        }
    }

    /// <summary>�x������ </summary>
    /// <param name="delayTime">�ҋ@����</param>
    /// <param name="token">�L�����Z���g�[�N��</param>
    async UniTask DelayTimeAsync(float delayTime, CancellationToken token)
    {
        Debug.Log("�J�n");
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: token);
        Debug.Log($"{delayTime}�o�� �I��");
    }

    /// <summary>���t���[���҂� </summary>
    async UniTask DelayFrameAsync(int delayFrame, CancellationToken token)
    {
        await UniTask.DelayFrame(delayFrame, cancellationToken: token);

        // Update�̃^�C�~���O��1�t���[���ҋ@����
        await UniTask.Yield();
    }

    /// <summary>�w�肵��������true�ɂȂ�܂őҋ@</summary>
    async UniTask WaitUntil(CancellationToken token)
    {
        Debug.Log("space�L�[����");
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: token);
        Debug.Log("�����ꂽ");
    }

    /// <summary>�w�肵��������false�ɂȂ�܂őҋ@ </summary>
    async UniTask WaitWhile(CancellationToken token)
    {
        var flag = true;
        await UniTask.WaitWhile(() => !flag, cancellationToken:token);
        Debug.Log("false�ɂȂ�܂���");
    }
}
