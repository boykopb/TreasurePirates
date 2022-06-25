using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace Items
{
  public class CoinCounterUI : MonoBehaviour
  {
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private protected TextMeshProUGUI _currentValueText;
    [SerializeField] private float _lerpRate = 0.01f;

    private int _currentValue;


    private void Start()
    {
      _coinManager.OnCoinCollect += UpdateNewValue;
      UpdateCurrentUIText();
    }

    
    private void OnDestroy()
    {
      _coinManager.OnCoinCollect -= UpdateNewValue;
    }

    private void UpdateCurrentUIText() =>
      _currentValueText.text = _currentValue.ToString();


    private void UpdateNewValue(int currentCountCoin)
    {
      StartCoroutine(SmoothIncreaseValueRoutine());
    }


    private IEnumerator SmoothIncreaseValueRoutine()
    {
      while (_currentValue !=_coinManager.CoinsCollectedCount)
      {
        _currentValue++;
        UpdateCurrentUIText();
        yield return new WaitForSeconds(_lerpRate);
      }
    }
  }
}