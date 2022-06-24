using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PirateCounter : MonoBehaviour
{
    [field: SerializeField]public int Count { private set; get; }

    private void Start()
    {
        EventManager.Current.OnChangedCountPirate += OnChangedCountPirate;
    }

    private void OnChangedCountPirate(int value)
    {
        Count += value;
        if (Count <= 0)
        {
            Count = 0;
            EventManager.Current.GameOver();
            //return;
        }
        
        EventManager.Current.ChangedCurrentValue(Count);
    }
}
