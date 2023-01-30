using System;
using System.Collections.Generic;
using UnityEngine;

public class Unsubscriber : MonoBehaviour, IDisposable
{
    //発行先リスト
    private List<IObserver<int>> _observers = new List<IObserver<int>>();
    //削除されたときにRemoveするobserver
    private IObserver<int> _observer;

    /// <summary>初期化処理</summary>
    /// <param name="observers">発行先リスト</param>
    /// <param name="observer">削除されたときにRemoveするobserver</param>
    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    /// <summary>削除を行う </summary>
    public void Dispose()
    {
        //Disposeされたら発行先リストから対象の発行先を削除する
        _observers.Remove(_observer);
    }
}
