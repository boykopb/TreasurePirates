using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private float _timeReload = 1f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _timeToDestroy = 10f;
    [SerializeField] private int _countDamagePirate = 1;
    private Boat _boat;
    
    void Start()
    {
        StartCoroutine(Attack());
        
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
        GameObject bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, transform.rotation);
        if (bulletObj.TryGetComponent(out Bullet bullet))
        {
            bullet.SetPirateDamage(-_countDamagePirate);
        }
        Destroy(bulletObj, _timeToDestroy);
    }
}
