using System;
using UnityEngine;

public class Observer : MonoBehaviour, IObserver<int>
{
    //データの発行が完了したことを通知する
    public void OnCompleted()
    {
        Debug.Log($"{gameObject.name}が通知の受け取りました");
    }

    //データの発行元でエラーが発生したことを通知する
    public void OnError(Exception error)
    {
        Debug.Log($"{gameObject.name}が次のエラーを受信しました。");
        Debug.Log($"{error}");
    }

    //データを通知する
    public void OnNext(int value)
    {
        Debug.Log($"{gameObject.name}が{value}を受け取りました");
    }
}
