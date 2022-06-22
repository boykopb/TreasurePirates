using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField] private Transform _target;
  [SerializeField] private bool _isCameraMovementXRestricted = true;
  [SerializeField] private float _maxPositionX = 1f;
  [SerializeField] private float _lerpRate = 3f;

  private void Update()
  {
    if (_target == null)
      return;

    if (!_isCameraMovementXRestricted)
    {
      transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);
    }
    else
    {
      var nextPosition = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);
      nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxPositionX, _maxPositionX);
      transform.position = nextPosition;
    }
  }



  public void SetTarget(Transform newTarget)
  {
    _target = newTarget;
  }
}