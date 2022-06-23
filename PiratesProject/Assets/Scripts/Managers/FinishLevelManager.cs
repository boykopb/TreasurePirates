using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Managers
{
  public class FinishLevelManager : MonoBehaviour
  {
    [SerializeField] private FinishLineTrigger _finishLine;

    [Header("Boat params")] 
    [SerializeField] private Transform _boatTransform;
    [SerializeField] private Transform _stopBoatPoint;
    [SerializeField] private float _lerpRateToStopPosition = 0.8f;

    [Header("On finish effects params")] 
    [SerializeField] private ParticleSystem[] _onFinishConfettiVFX;
    [SerializeField] private AudioClip _onFinishConfettiSFX;
    [SerializeField] private float _fxCooldownPeriod = 0.2f;
    [SerializeField] private MMF_Player _onStopCameraLightShake;
    [SerializeField] private float _waitTimeBeforeFinishLevel = 0.8f;
  

    private Movement _boatMovement;
    private bool _isFinishLineReached;
    private bool _isBoatMovedToStopPosition;

    public event Action OnLevelFinishedEvent;

    private void Start()
    {
      _finishLine.OnFinishLineReachedEvent += OnFinishLineLineReach;

      _boatMovement = _boatTransform.GetComponent<Movement>();
    }

    private void Update()
    {
      if (!_isFinishLineReached || _isBoatMovedToStopPosition)
        return;
      LerpBoatToStopPoint();
    
      if (IsBoatReachedStopPoint()) 
        DoActionsOnBoatStop();
    }

    private void OnDestroy()
    {
      _finishLine.OnFinishLineReachedEvent -= OnFinishLineLineReach;
    }

    private void LerpBoatToStopPoint()
    {
      _boatTransform.position = Vector3.Lerp(_boatTransform.position, _stopBoatPoint.position, Time.deltaTime * _lerpRateToStopPosition);
      _boatTransform.rotation = Quaternion.Lerp(_boatTransform.rotation, _stopBoatPoint.rotation, Time.deltaTime * _lerpRateToStopPosition);
    }

    private void DoActionsOnBoatStop()
    {
      _isBoatMovedToStopPosition = true;
      _onStopCameraLightShake.PlayFeedbacks();
      StartCoroutine(InvokeFinishLevel());
    }

    private IEnumerator InvokeFinishLevel()
    {
      yield return new WaitForSeconds(_waitTimeBeforeFinishLevel);
      OnLevelFinishedEvent?.Invoke();
    }

    private bool IsBoatReachedStopPoint() => 
      Vector3.Distance(_boatTransform.position, _stopBoatPoint.position) <= 0.5f;

    private void OnFinishLineLineReach()
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
}