using UnityEngine;

namespace Items
{
  public class ItemAnimator : MonoBehaviour
  {
    [SerializeField] private float _rotationSpeedX = 0f;
    [SerializeField] private float _rotationSpeedY = 1f;
    [SerializeField] private float _rotationSpeedZ = 0f;
    [SerializeField] private float _floatingAmplitude = 0.2f;
    [SerializeField] private float _floatingSpeed = 1f;

    private Vector3 _startPosition;
    private Vector3 _tempPos;

    private void Start()
    {
      _startPosition = transform.localPosition;
    }

    void Update()
    {
      Rotate();
      Float();
    }


    private void Rotate()
    {
      var rotation = new Vector3(_rotationSpeedX, _rotationSpeedY, _rotationSpeedZ);
      transform.Rotate(rotation * Time.deltaTime);
    }

    private void Float()
    {
      _tempPos = _startPosition;
      _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _floatingSpeed) * _floatingAmplitude;

      transform.localPosition = _tempPos;
    }
  }
}