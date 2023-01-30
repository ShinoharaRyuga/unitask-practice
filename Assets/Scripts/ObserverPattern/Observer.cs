using System;
using UnityEngine;

public class Observer : MonoBehaviour, IObserver<int>
{
    //�f�[�^�̔��s�������������Ƃ�ʒm����
    public void OnCompleted()
    {
        Debug.Log($"{gameObject.name}���ʒm�̎󂯎��܂���");
    }

    //�f�[�^�̔��s���ŃG���[�������������Ƃ�ʒm����
    public void OnError(Exception error)
    {
        Debug.Log($"{gameObject.name}�����̃G���[����M���܂����B");
        Debug.Log($"{error}");
    }

    //�f�[�^��ʒm����
    public void OnNext(int value)
    {
        Debug.Log($"{gameObject.name}��{value}���󂯎��܂���");
    }
}
