using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _effectDeathPirate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pirate pirate))
        {
            CreateEffect(other.transform.position);
        }
    }

    private void CreateEffect(Vector3 spawnPos)
    {
        Instantiate(_effectDeathPirate, spawnPos, Quaternion.identity);
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
