using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private float _timeReload = 1f;
    [SerializeField] private float _timeToSpawnBullets = 2f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _timeToDestroy = 10f;
    [SerializeField] private int _countDamagePirate = 1;
    [SerializeField] private ParticleSystem _fireEffect;
    private Boat _boat;
    
    void Start()
    {
        StartCoroutine(Attack());
        
    }
    
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_timeToSpawnBullets);
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
        _fireEffect.Play();
        Destroy(bulletObj, _timeToDestroy);
    }
}
