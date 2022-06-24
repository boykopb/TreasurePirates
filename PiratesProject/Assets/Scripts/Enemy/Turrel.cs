using System.Collections;
using Player;
using UnityEngine;

namespace Enemy
{
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
        [SerializeField] private bool _isFire = false;
        private float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= _timeReload && gameObject.activeSelf)
            {
                timer = 0;
                SpawnBullet();
            }
        }

        IEnumerator Attack()
        {
            //yield return new WaitForSeconds(_timeToSpawnBullets);
            while (true)
            {
                _isFire = true;
                Debug.Log("Attack");
            
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
}
