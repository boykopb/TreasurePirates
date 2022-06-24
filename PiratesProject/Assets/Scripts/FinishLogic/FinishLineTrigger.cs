using System;
using Player;
using UnityEngine;

namespace FinishLogic
{
  public class FinishLineTrigger : MonoBehaviour
  {
    [SerializeField] private GameObject _finishLineGameObject;
    private bool _isHit;

    public event Action OnFinishLineReachedEvent;

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent<BoatTrigger>(out _) || _isHit)
        return;

      _isHit = true;

      OnFinishLineReachedEvent?.Invoke();
      _finishLineGameObject.SetActive(false);
    }
  }
}