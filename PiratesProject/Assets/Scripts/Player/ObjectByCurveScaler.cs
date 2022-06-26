using System.Collections;
using UnityEngine;

namespace Player
{
  public enum ExecuteType
  {
    OnStart,
    OnDemand
  }

  public class ObjectByCurveScaler : MonoBehaviour
  {
    [SerializeField] private protected ExecuteType _executeType = ExecuteType.OnStart;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Vector3 _startScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private float _lerpRate = 1f;

    private bool _isProcessActive;

    private void Start()
    {
      if (_executeType == ExecuteType.OnStart)
        ChangeScale();
    }


    public void ChangeScale()
    {
      if (!_isProcessActive)
        StartCoroutine(ChangeScaleRoutine());
    }


    private IEnumerator ChangeScaleRoutine()
    {
      _isProcessActive = true;
      var endScale = transform.localScale;

      for (float t = 0; t < 1f; t += Time.deltaTime / _lerpRate)
      {
        transform.localScale = Vector3.Lerp(_startScale, endScale, _animationCurve.Evaluate(t));
        yield return null;
      }

      transform.localScale = endScale;
      _isProcessActive = false;
    }
  }
}