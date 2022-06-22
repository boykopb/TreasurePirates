using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Action<int> OnChangedValue;
    public Action OnGameOver;

    public static EventManager Current;

    private void Awake()
    {
        Current = this;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        OnGameOver?.Invoke();
    }

    public void ChangedValue(int currentValue)
    {
        OnChangedValue?.Invoke(currentValue);
    }
}
