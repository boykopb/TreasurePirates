using System;
using System.Collections;
using Managers;
using UnityEngine;

public class FinishLevelManager : MonoBehaviour
{
  [SerializeField] private FinishLineTrigger _finishLine;

  [Header("Boat params")] [SerializeField]
  private Transform _boatTransform;

  [SerializeField] private Transform _stopBoatPoint;
  [SerializeField] private float _lerpRateToStopPosition = 0.5f;

  [Header("On finish effects")] [SerializeField]
  private ParticleSystem[] _onFinishConfettiVFX;

  [SerializeField] private AudioClip _onFinishConfettiSFX;
  [SerializeField] private float _fxCooldownPeriod = 0.1f;


  private Movement _boatMovement;
  private bool _isFinishLineReached;
  private bool _isBoatMovedToStopPosition;

  private void Start()
  {
    _finishLine.OnFinishReachedEvent += OnFinishLineReach;

    _boatMovement = _boatTransform.GetComponent<Movement>();
  }

  private void Update()
  {
    MovePlayerBoatToStopPoint();
  }

  private void MovePlayerBoatToStopPoint()
  {
    if (!_isFinishLineReached || _isBoatMovedToStopPosition)
      return;

    _boatTransform.position = Vector3.Lerp(_boatTransform.position, _stopBoatPoint.position, Time.deltaTime * _lerpRateToStopPosition);
    _boatTransform.rotation = Quaternion.Lerp(_boatTransform.rotation, _stopBoatPoint.rotation, Time.deltaTime * _lerpRateToStopPosition);


    if (Vector3.Distance(_boatTransform.position, _stopBoatPoint.position) <= 0.5f)
    {
      _isBoatMovedToStopPosition = true;
      Debug.Log("_isBoatMovedToStopPosition = true");
    }
  }

  private void OnDestroy()
  {
    _finishLine.OnFinishReachedEvent -= OnFinishLineReach;
  }

  private void OnFinishLineReach()
  {
    _isFinishLineReached = true;
    DisableBoatControl();
    StartCoroutine(ConfettiFXPlay());
  }

  private void DisableBoatControl()
  {
    _boatMovement.enabled = false;
  }

  private IEnumerator ConfettiFXPlay()
  {
    for (var i = 0; i < _onFinishConfettiVFX.Length; i++)
    {
      _onFinishConfettiVFX[i].Play();
      SoundManager.Instance.PlaySFX(_onFinishConfettiSFX);
      yield return new WaitForSeconds(_fxCooldownPeriod);
    }
  }
}