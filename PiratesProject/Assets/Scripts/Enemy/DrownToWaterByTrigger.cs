using System.Collections;
using Managers;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DrownToWaterByTrigger : MonoBehaviour
{
  [Header("Controlled components")] 
  [SerializeField] private MonoBehaviour[] _setOffComponents;

  [Header("Params")] 
  [SerializeField, Range(0, 5)] protected int _countPirateToDestroy = 1;
  [SerializeField] private Vector3 _endRotation = new Vector3(-120f, 180f, 0);
  [SerializeField] private float _endPositionY = -1f;
  [SerializeField] private float _lerpRate = 15f;
  [SerializeField] private float _endScaleXYZ = 0.5f;
  [SerializeField] private float _objectDestroyTime = 3f;

  [Header("FX")] 
  [SerializeField] private AudioClip _waterDrownSfx;

  private Collider _collider;
  private bool _isHit;

  private void Start()
  {
    _collider = GetComponent<Collider>();
  }


  private void OnTriggerEnter(Collider other)
  {
    if (!other.TryGetComponent<BoatTrigger>(out _) || _isHit)
      return;

    _isHit = true;

    SetOffComponents();

    AudioManager.Instance.PlaySFX(_waterDrownSfx, 0.3f);
    EventManager.Current.ChangedCountPirate(-_countPirateToDestroy);

    StartCoroutine(DrownRoutine());
    Destroy(gameObject, _objectDestroyTime);
  }

  private void SetOffComponents()
  {
    _collider.enabled = false;

    for (var i = 0; i < _setOffComponents.Length; i++)
      _setOffComponents[i].enabled = false;
  }


  private IEnumerator DrownRoutine()
  {
    for (float t = 0; t < 1f; t += Time.deltaTime / _lerpRate)
    {
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_endRotation), t);
      transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _endPositionY, transform.position.z), t);
      transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_endScaleXYZ, _endScaleXYZ, _endScaleXYZ), t);

      yield return null;
    }
  }
}