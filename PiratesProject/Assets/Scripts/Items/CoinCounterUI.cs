using Managers;
using TMPro;
using UnityEngine;

namespace Items
{
  public class CoinCounterUI : MonoBehaviour
  {

    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private protected TextMeshProUGUI _currentValueText;

    private void Start()
    {
      SubscribeOnChange();
      UpdateCurrentUIText();
    }

    private void SubscribeOnChange() =>
      _coinManager.OnCoinCollect += UpdateCurrentUIText;

    private void UpdateCurrentUIText() =>
      _currentValueText.text = _coinManager.CoinsCollectedCount.ToString();
  }
}