using System;
using Player;
using UnityEngine;

namespace FinishLogic
{
  public class FinishLineTrigger : MonoBehaviour
  {
    [SerializeField] private GameObject _finishLineGameObject;
  
    public event Action OnFinishLineReachedEvent;

    private void OnTriggerEnter(Collider other)
    {
      if (other.attachedRigidbody.TryGetComponent<Movement>(out _))
      {
        OnFinishLineReachedEvent?.Invoke();
        _finishLineGameObject.SetActive(false);
      }
    }
  }
}