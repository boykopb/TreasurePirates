using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
  public class PlayerDeath : MonoBehaviour
  {
    [Header ("Effects")]
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] private float _destroyDelayTime = 3f;
    [SerializeField] private float _drownLerpRate = 70f;

    [Header("Player components to be off")]
    [SerializeField] private MonoBehaviour[] _playerComponentsToOff;
    [SerializeField] private Collider[] _playerColliders;
    
    private bool _isDead;
    
    private void Start()
    {
      
      EventManager.Current.OnGameOver += PlayDeath;
    }

    private void PlayDeath()
    {
      if (_isDead) return;

      _isDead = true;
      SetOffComponents();
      PlayVFX();
      AudioManager.Instance.PlaySFX(_deathSFX);
      StartCoroutine(DrownRoutine());
      StartCoroutine(DeactivateRoutine());
    }

    private IEnumerator DeactivateRoutine()
    {
      yield return new WaitForSeconds(_destroyDelayTime);
      gameObject.SetActive(false);
    }

    private void PlayVFX()
    {
      var position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
      Instantiate(_deathVFX, position, Quaternion.identity);
    }

    private void SetOffComponents()
    {
      
      for (var i = 0; i < _playerColliders.Length; i++)
      {
        _playerColliders[i].enabled = false;
      }
      
      for (var i = 0; i < _playerComponentsToOff.Length; i++)
      {
        _playerComponentsToOff[i].enabled = false;
      }
    }


    private IEnumerator DrownRoutine()
    {
      for (float t = 0; t < 1f; t += Time.deltaTime / _drownLerpRate)
      {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -1, transform.position.z), t);
        yield return null;
      }
    }
  }
}