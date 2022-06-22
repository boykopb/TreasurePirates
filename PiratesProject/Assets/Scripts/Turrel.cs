using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _timeReload = 1f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _timeToDestroy = 10f;
    [SerializeField] private float _countPirate = 1f;
    private Boat _boat;
    
    void Start()
    {
        StartCoroutine(Attack());
        
    }

    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            SpawnBullet();
            yield return new WaitForSeconds(_timeReload);
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _spawnPosition.position, transform.rotation);
        Destroy(bullet, _timeToDestroy);
    }
}
