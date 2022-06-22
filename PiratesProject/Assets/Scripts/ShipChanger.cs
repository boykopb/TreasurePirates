using System.Collections.Generic;
using UnityEngine;

public class ShipChanger : MonoBehaviour
{
  [SerializeField] private List<GameObject> _ships;

  private int _currentShip = 0;


  public void ChangeShip()
  {
    _currentShip = _currentShip == _ships.Count - 1 ? 0 :_currentShip + 1;

    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }
  
  
  public void UpgradeShip()
  {
    if (_currentShip == _ships.Count - 1) 
      return;
    
    _currentShip++;
    
    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }
  
  
  public void DowngradeShip()
  {
    if (_currentShip == 0) 
      return;
    
    _currentShip--;
    
    for (var i = 0; i < _ships.Count; i++)
    {
      _ships[i].SetActive(i == _currentShip);
    }
  }
}