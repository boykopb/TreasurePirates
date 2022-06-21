using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [field: SerializeField]public int CountPirates { private set; get; }
    
    public void ChangeCountPirate(int value)
    {
        CountPirates += value;
        if (CountPirates <= 0)
        {
            CountPirates = 0;
            EventManager.Current.GameOver();
            return;
        }
        EventManager.Current.ChangedValue(CountPirates);
        Debug.Log("CountPirate = " + CountPirates);
    }
}
