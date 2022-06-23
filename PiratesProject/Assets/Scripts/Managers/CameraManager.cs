using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
  public class CameraManager : MonoBehaviour
  {
    [Header("Controlled components")] [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private FinishLevelManager _finishLevelManager;

    [Header("Camera finish params")] [SerializeField]
    private float _onFinishFOV = 80;

    [SerializeField] private Vector3 _onFinishPosition = new Vector3(0f, 3f, -4f);
    [SerializeField] private Vector3 _onFinishRotation = new Vector3(15f, 0f, 0f);
    [SerializeField] private float _cameraLerpRate = 3f;

    [Header("Finish targets")] [SerializeField]
    private Transform[] _cameraTargets;

    private bool _isLevelFinished;
    private bool _isCameraFinishedSwitching;

    public event Action OnFinishGameCameraSwitchedEvent;

    private void Start()
    {
      _finishLevelManager.OnLevelFinishedEvent += OnLevelFinish;
    }

    private void OnLevelFinish()
    {
      _isLevelFinished = true;
    }

    private void Update()
    {
      if (_isLevelFinished && !_isCameraFinishedSwitching)
        SwitchToEndLevelView();
    }

    private void SwitchToEndLevelView()
    {
      _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_virtualCamera.m_Lens.FieldOfView, _onFinishFOV, Time.deltaTime * _cameraLerpRate);
      _virtualCamera.transform.localPosition = Vector3.Lerp(_virtualCamera.transform.localPosition, _onFinishPosition, Time.deltaTime * _cameraLerpRate);
      _virtualCamera.transform.localRotation = Quaternion.Lerp(_virtualCamera.transform.localRotation, Quaternion.Euler(_onFinishRotation), Time.deltaTime * _cameraLerpRate);

      if (Vector3.Distance(_virtualCamera.transform.localPosition, _onFinishPosition) < 0.05f)
      {
        _isCameraFinishedSwitching = true;
        OnFinishGameCameraSwitchedEvent?.Invoke();
      }
    }

    public void SetNextTarget(int targetIndex)
    {
      if (targetIndex > _cameraTargets.Length - 1 || targetIndex < 0)
        return;

      _cameraFollow.SetTarget(_cameraTargets[targetIndex]);
    }
  }
}