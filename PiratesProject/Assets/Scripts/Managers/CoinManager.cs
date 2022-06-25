using System;
using Items;
using UnityEngine;

namespace Managers
{
  public class CoinManager : MonoBehaviour
  {
    private CoinItem[] _coinItemsOnScene;
  
    public int CoinsCollectedCount { get; private set; }

    public event Action<int> OnCoinCollect;
    
    private void Awake()
    {
      GetAndInitCoinsOnScene();
    }

    public void CollectCoin()
    {
      CoinsCollectedCount++;
      OnCoinCollect?.Invoke(CoinsCollectedCount);
    }

    private void GetAndInitCoinsOnScene()
    {
      _coinItemsOnScene = FindObjectsOfType<CoinItem>();

      for (var i = 0; i < _coinItemsOnScene.Length; i++) 
        _coinItemsOnScene[i].Construct(this);
    }


    public void MultiplyCoins(int multiplier)
    {
      CoinsCollectedCount *= multiplier;
      OnCoinCollect?.Invoke(CoinsCollectedCount);
    }
  }
}