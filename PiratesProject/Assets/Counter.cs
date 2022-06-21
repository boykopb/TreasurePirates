using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int CountPirates { private set; get; }
    
    public void ChangeCountPirate(int value)
    {
        CountPirates += value;
        EventManager.Current.ChangedValue(CountPirates);
        Debug.Log("CountPirate = " + CountPirates);
    }
}
