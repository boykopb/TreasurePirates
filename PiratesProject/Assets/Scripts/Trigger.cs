using System;
using System.Collections;
using System.Collections.Generic;
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
        if (other.TryGetComponent(out PirateCounter counter))
        {
            switch (_typeTrigger)
            {
                case TypeOfTrigger.Add:
                    break;
                case TypeOfTrigger.Remove:
                    _countPirate *= -1;
                    break;
            }
            ChangeValue(counter);
            Destroy();
        }
    }

    private void ChangeValue(PirateCounter pirateCounter)
    {
        pirateCounter.ChangeCountPirate(_countPirate);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
