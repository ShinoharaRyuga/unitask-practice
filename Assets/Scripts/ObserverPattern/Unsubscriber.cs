using System;
using System.Collections.Generic;
using UnityEngine;

public class Unsubscriber : MonoBehaviour, IDisposable
{
    //���s�惊�X�g
    private List<IObserver<int>> _observers = new List<IObserver<int>>();
    //�폜���ꂽ�Ƃ���Remove����observer
    private IObserver<int> _observer;

    /// <summary>����������</summary>
    /// <param name="observers">���s�惊�X�g</param>
    /// <param name="observer">�폜���ꂽ�Ƃ���Remove����observer</param>
    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    /// <summary>�폜���s�� </summary>
    public void Dispose()
    {
        //Dispose���ꂽ�甭�s�惊�X�g����Ώۂ̔��s����폜����
        _observers.Remove(_observer);
    }
}
