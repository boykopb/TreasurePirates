using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    [SerializeField] private float _forceJump = 10f;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void GetForceAfterDeath(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _forceJump, ForceMode.Acceleration);
    }
    
}
