using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

enum TypeOfTrigger
{
    Add,
    Remove
}
public class Trigger : MonoBehaviour
{
    [SerializeField] protected int _countPirate;
    [SerializeField] private TypeOfTrigger _typeTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoatTrigger boatTrigger))
        {
            switch (_typeTrigger)
            {
                case TypeOfTrigger.Add:
                    break;
                case TypeOfTrigger.Remove:
                    _countPirate *= -1;
                    break;
            }
            ChangeValue();
            Destroy();
        }
    }

    private void ChangeValue()
    {
        EventManager.Current.ChangedCountPirate(_countPirate);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
