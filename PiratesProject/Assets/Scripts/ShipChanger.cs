using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ShipChanger : MonoBehaviour
{
  [SerializeField] private List<GameObject> _ships;
  [SerializeField] private int[] _countPirateLevel = new []{0,2,4,8};

  [SerializeField] private int _minBorder = 0;
  private int _currentShip = 0;
  private int _currentCount;
  private int _nextLevelBorder;
  private int _prevLevelBorder = 0;

  private void Start()
  {
    SetBorder(_currentShip);
    EventManager.Current.OnChangedCurrentValue += OnChangedCurrentValue;
  }

  private void OnChangedCurrentValue(int value)
  {
    if (value < _prevLevelBorder)
      DowngradeShip();
    else if (value > _nextLevelBorder)
      UpgradeShip();

    SetBorder(_currentShip);
    
  }

  public void ChangeShip()
  {
    _currentShip = _currentShip == _ships.Count - 1 ? 0 :_currentShip + 1;

    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }


  private void UpgradeShip()
  {
    if (_currentShip == _ships.Count - 1) 
      return;
    
    _currentShip++;
    
    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }


  private void DowngradeShip()
  {
    if (_currentShip == 0) 
      return;
    
    _currentShip--;
    
    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }

  private void SetBorder(int newIndex)
  {
    int prevIndex = newIndex == _minBorder ? newIndex : _currentShip;
    _prevLevelBorder = _countPirateLevel[prevIndex];
    
    int nextIndex = newIndex == _countPirateLevel.Length - 1 ? newIndex : _currentShip + 1;
    _nextLevelBorder = _countPirateLevel[nextIndex];
  }
}