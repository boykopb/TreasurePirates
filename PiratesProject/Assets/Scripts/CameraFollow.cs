using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField] private Transform _target;
  [SerializeField] private float _maxPositionX = 1.5f;

  [SerializeField] private float _lerpRate = 5f;

  private void Update()
  {
    if (_target == null)
      return;

    //cameraMovement without restriction
    //  transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);

    //cameraMovement with restriction
    var nextPosition = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);
    nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxPositionX, _maxPositionX);
    transform.position = nextPosition;
  }
}