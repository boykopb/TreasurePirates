using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCounter : MonoBehaviour
{
    [field: SerializeField]public int Count { private set; get; }
    
    public void ChangeCountPirate(int value)
    {
        Count += value;
        if (Count <= 0)
        {
            Count = 0;
            EventManager.Current.GameOver();
            return;
        }
        
        EventManager.Current.ChangedValue(Count);
        //Debug.Log("CountPirate = " + CountPirates);
    }
}
