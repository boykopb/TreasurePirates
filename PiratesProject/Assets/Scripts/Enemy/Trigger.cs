using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using Unity.Mathematics;
using UnityEngine;

namespace Enemy
{
    enum TypeOfTrigger
    {
        Add,
        Remove
    }
    public class Trigger : MonoBehaviour
    {
        [SerializeField] protected int _countPirate;
        [SerializeField] private TypeOfTrigger _typeTrigger;
        [SerializeField] private bool _isDiedAfterTrigger = true;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private GameObject _effectAfterTrigger;
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
                if(_isDiedAfterTrigger)
                    Destroy();
            }
        }

        private void ChangeValue()
        {
            EventManager.Current.ChangedCountPirate(_countPirate);
        }

        private void Destroy()
        {
            if(_effectAfterTrigger)
                Instantiate(_effectAfterTrigger, transform.position, Quaternion.identity);
            if(_audio)
                _audio.Play();
            Destroy(gameObject);
        }
    }
}