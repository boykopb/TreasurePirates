using Cinemachine;
using UnityEngine;

namespace Managers
{
  public class CameraManager : MonoBehaviour
  {
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _onFinishFOV = 65;
    [SerializeField] private Vector3 _onFinishPosition = new Vector3(0f, 9f, -4f);
    [SerializeField] private Vector3 _onFinishRotation = new Vector3(40f, 0f, 0f);
    [SerializeField] private Transform[] _cameraTargets;

    [SerializeField] private FinishLevelManager _finishLevelManager;

    private int _currentTarget = -1;
    private float _lerpRate = 3f;
    private bool _isLevelFinished;

    private void Start()
    {
      _finishLevelManager.OnLevelFinishedEvent += OnLevelFinish;
    }

    private void OnLevelFinish()
    {
      _isLevelFinished = true;
      SetNextTarget();
    }


    private void Update()
    {
      if (_isLevelFinished)
        SetFinishCameraSettings();

      if (Input.GetKeyDown(KeyCode.N))
      {
        SetNextTarget();
      }
    }

    private void SetFinishCameraSettings()
    {
      _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_virtualCamera.m_Lens.FieldOfView, _onFinishFOV, Time.deltaTime * _lerpRate);

      _virtualCamera.transform.localPosition = Vector3.Lerp(_virtualCamera.transform.localPosition, _onFinishPosition, Time.deltaTime * _lerpRate);

      _virtualCamera.transform.localRotation = Quaternion.Lerp(_virtualCamera.transform.localRotation, Quaternion.Euler(_onFinishRotation), Time.deltaTime * _lerpRate);
    }


    public void SetNextTarget()
    {
      _currentTarget++;
      if (_currentTarget > _cameraTargets.Length - 1)
        return;

      _cameraFollow.SetTarget(_cameraTargets[_currentTarget]);
    }
  }
}