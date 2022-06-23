using System.Collections;
using UnityEngine;

namespace Managers
{
  public class BonusManager : MonoBehaviour
  {
    [Header("Controlled components")] [SerializeField]
    private CameraManager _cameraManager;

    [SerializeField] private PirateCounter _pirateCounter;
    [SerializeField] private CoinManager _coinManager;

    [Header("Params")] [SerializeField] private float _timeBeforeCameraChange = 0.6f;
    [SerializeField] private float _timeBeforeNextBonus = 1.8f;

    [Header("FX")] [SerializeField] private GameObject[] _treasures;
    [SerializeField] private AudioClip _goldMultiplySfx;


    private void Start()
    {
      _cameraManager.OnFinishGameCameraSwitchedEvent += CalculateAndShowBonuses;
    }

    private void OnDestroy()
    {
      _cameraManager.OnFinishGameCameraSwitchedEvent -= CalculateAndShowBonuses;
    }

    private void CalculateAndShowBonuses()
    {
      StartCoroutine(BonusesRoutine());
    }


    private IEnumerator BonusesRoutine()
    {
      for (var i = 0; i < _pirateCounter.Count; i++)
      {
        _cameraManager.SetNextTarget(i);
        yield return new WaitForSeconds(_timeBeforeCameraChange);
        _treasures[i].SetActive(true);
        yield return new WaitForSeconds(_timeBeforeNextBonus);
      }

      if (_pirateCounter.Count > 1)
        MultiplyCoins();
    }

    private void MultiplyCoins()
    {
      _coinManager.MultiplyCoins(_pirateCounter.Count);
      SoundManager.Instance.PlaySFX(_goldMultiplySfx);
    }
  }
}