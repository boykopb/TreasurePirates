using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private float _movementSpeed = 3f;
  [SerializeField] private float _maxAngle = 50f;
  [SerializeField] private float _maxPosition = 3.5f;
  [SerializeField] private float _rotationSensitivity = 0.5f;

  private Vector3 _oldPosition;
  private float _yAngle;


  private void Update()
  {
    ClampedMovementBehavour();

    if (Input.GetMouseButtonDown(0))
      _oldPosition = Input.mousePosition;


    if (Input.GetMouseButton(0))
      RotatePlayer();
  }

  private void ClampedMovementBehavour()
  {
    var movementOffset = transform.forward * (_movementSpeed * Time.deltaTime);
    var nextPosition = transform.position + movementOffset;
    nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxPosition, _maxPosition);
    transform.position = nextPosition;
  }


  private void RotatePlayer()
  {
    var deltaMousePosition = Input.mousePosition - _oldPosition;

    _oldPosition = Input.mousePosition;
    _yAngle += deltaMousePosition.x * _rotationSensitivity;

    _yAngle = Mathf.Clamp(_yAngle, -_maxAngle, _maxAngle);

    transform.rotation = Quaternion.Euler(new Vector3(0, _yAngle, 0));
  }
}