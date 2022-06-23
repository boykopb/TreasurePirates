using System.Collections;
using UnityEngine;

namespace Managers
{
  public class BonusManager:MonoBehaviour
  {
    [SerializeField] private CameraManager _cameraManager;
  //  [SerializeField] private PirateCounter _pirateCounter;
    [SerializeField] private GameObject [] _treasures;
    [SerializeField] private float _timeBeforeCameraChange = 0.7f;
    [SerializeField] private float _timeBeforeNextBonus = 2f;

    //+ надо CoinManager => MultiplyCoins
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
      for (var i = 0; i < 7/*_pirateCounter.Count*/; i++)
      {
        _cameraManager.SetNextTarget(i);
        yield return new WaitForSeconds(_timeBeforeCameraChange);
        _treasures[i].SetActive(true);
        yield return new WaitForSeconds(_timeBeforeNextBonus);
      }
    }
  }
}