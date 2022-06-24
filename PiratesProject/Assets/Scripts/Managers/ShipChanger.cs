using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Managers
{
  public class ShipChanger : MonoBehaviour
  {
    [SerializeField] private List<GameObject> _ships;
    [SerializeField] private PirateCounter _pirateCounter;
    [SerializeField] private int _minBorder = 0;

    
    [Header("Effects")]
    [SerializeField] private AudioClip _upgradeSFX;
    [SerializeField] private GameObject _upgradeVFX;
    [SerializeField] private ObjectByCurveScaler _curveScaler;
    
    
    private int[] _countPirateLevel;
    private int _currentShip = 0;
    private int _currentCount;
    private int _nextLevelBorder;
    private int _prevLevelBorder = 0;

    public event Action OnUpgradeShipEvent;
    public event Action OnDowngradeShipEvent;


    private void Start()
    {
      _countPirateLevel = _pirateCounter.CountPirateLevel;
      SetBorder(_currentShip);
    }

    public void OnChangedCurrentValue(int value)
    {
      if (value <= _prevLevelBorder)
        DowngradeShip();
      else if (value > _nextLevelBorder)
        UpgradeShip();

      SetBorder(_currentShip);
    }

    public void ChangeShip()
    {
      _currentShip = _currentShip == _ships.Count - 1 ? 0 : _currentShip + 1;

      for (var i = 0; i < _ships.Count; i++)
      {
        _ships[i].SetActive(i == _currentShip);
      }
    }
    
    public int GetCurrentShipIndex()
    {
      return _currentShip;
    }

    private void UpgradeShip()
    {
      if (_currentShip == _ships.Count - 1)
      {
        EventManager.Current.ChangedCurrentValue(_countPirateLevel[^1]);
        return;
      }

      _currentShip++;
      for (var i = 0; i < _ships.Count; i++)
      {
        _ships[i].SetActive(i == _currentShip);
      }

      OnUpgradeShipEvent?.Invoke();
      VisualEffectsOnUpgrade();
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

      OnDowngradeShipEvent?.Invoke();
      VisualEffectsOnDowngrade();
    }

    private void SetBorder(int newIndex)
    {
      int prevIndex = newIndex == _minBorder ? newIndex : _currentShip;
      _prevLevelBorder = _countPirateLevel[prevIndex];

      int nextIndex = newIndex == _countPirateLevel.Length - 1 ? newIndex : _currentShip + 1;
      _nextLevelBorder = _countPirateLevel[nextIndex];
    }
    
    
    private void VisualEffectsOnDowngrade()
    {
      _curveScaler.ChangeScale();
    }
    
    private void VisualEffectsOnUpgrade()
    {
      _curveScaler.ChangeScale();
      PlayVFX(_upgradeVFX);
      AudioManager.Instance.PlaySFX(_upgradeSFX);
    }

    

    private void PlayVFX(GameObject vfx)
    {
      var position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
      Instantiate(vfx, position, Quaternion.Euler(new Vector3(-80, 90, 0)));
      Instantiate(vfx, position, Quaternion.Euler(new Vector3(-105, 90, 0)));
    }
  }
}