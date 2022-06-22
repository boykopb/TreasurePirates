using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private int _countDamagePirate = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PirateCounter counter))
        {
            counter.ChangeCountPirate(_countDamagePirate);
            Destroy();
        }
    }

    private void Destroy()
    {
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void SetPirateDamage(int value)
    {
        _countDamagePirate = value;
    }
}
