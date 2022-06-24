using Managers;
using Player;
using UnityEngine;

namespace Enemy
{
    enum TypeOfTrigger
    {
        Add,
        Remove
    }
    public class OnEnemyTrigger : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected int _countPirate;
        [SerializeField] private TypeOfTrigger _typeTrigger;
        [SerializeField] private bool _isDiedAfterTrigger = true;
        [SerializeField] private float _destroyDelayTime;
        
        [Header("Effects")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private GameObject _effectAfterTrigger;
        [SerializeField] private float _audioClipPlayDelayTime;
      
        
        private bool _isHit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoatTrigger boatTrigger) || !_isHit)
            {
                _isHit = true;
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
            
            if(_audioClip)
                AudioManager.Instance.PlaySFX(_audioClip, _audioClipPlayDelayTime);
            
            Destroy(gameObject, _destroyDelayTime);
        }
    }
}