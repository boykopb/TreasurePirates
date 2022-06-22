using System;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
  public event Action OnFinishReachedEvent;

  private void OnTriggerEnter(Collider other)
  {
    if (other.attachedRigidbody.TryGetComponent<Movement>(out _)) 
      OnFinishReachedEvent?.Invoke();
  }
}