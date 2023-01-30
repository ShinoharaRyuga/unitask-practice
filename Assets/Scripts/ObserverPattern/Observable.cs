using System;
using System.Collections.Generic;
using UnityEngine;

public class Observable : MonoBehaviour, IObservable<int>
{
    [SerializeField]
    List<Observer> _observerObjects = new List<Observer>();

    /// <summary>çwì«Ç≥ÇÍÇΩIObserverÇÃÉäÉXÉg </summary>
    List<IObserver<int>> _observers = new List<IObserver<int>>();

    List<IDisposable> _disposables = new List<IDisposable>();

    void Start()
    {
        foreach (var observerObject in _observerObjects)
        {
            _observers.Add(observerObject);
            IDisposable disposable = this.Subscribe(observerObject);
            _disposables.Add(disposable);
        }

        SendNotice();
        _disposables[0].Dispose();
        SendNotice();
    }

    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer);
    }


    void SendNotice()
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
        }
    }
}
