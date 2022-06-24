using Managers;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _deathEffect;
    
        private int _countDamagePirate = 0;
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoatTrigger triggerBoat))
            {
                EventManager.Current.ChangedCountPirate(_countDamagePirate);
                Destroy();
            }
        }

        private void Destroy()
        {
            ActivateDeathEffects();
            Destroy(gameObject);
        }

        private void ActivateDeathEffects()
        {
            Instantiate(_deathEffect,transform.position, Quaternion.identity);
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
}
