using Managers;
using UnityEngine;

namespace Items
{
  public class CoinItem : MonoBehaviour
  {
    [SerializeField] private protected GameObject _onPickUpVFX;
    [SerializeField] private protected AudioClip _onPickUpSFX;

    private CoinManager _coinManager;
    private bool _isTaken;

    public void Construct(CoinManager coinManager)
    {
      _coinManager = coinManager;
    }


    private void OnTriggerEnter(Collider other)
    {
      if (!other.attachedRigidbody.TryGetComponent<Movement>(out _) || _isTaken)
        return;

      PickUp(other.transform);
    }

    private void PickUp(Transform otherTransform)
    {
      _isTaken = true;
      _coinManager.CollectCoin();

      Instantiate(_onPickUpVFX, otherTransform);
      SoundManager.Instance.PlaySFX(_onPickUpSFX);
      Destroy(gameObject);
    }
  }
}