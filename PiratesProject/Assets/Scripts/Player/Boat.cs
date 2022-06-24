using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Player
{
  public class Boat : MonoBehaviour
  {
    [SerializeField] private PirateCounter pirateCounter;
    [SerializeField] private GameObject _prefabPirate;
    [SerializeField] private int _countInRow = 2;
    [SerializeField] private float _deltaZ = 5f;
    [SerializeField] private float _deltaX = 5f;
    [SerializeField] private Transform _startSpawnPos;
    [SerializeField] private float _forceToSide = 10f;
    [SerializeField] private Transform _parentForPirate;
    [SerializeField] private ShipChanger _shipChanger;

    [SerializeField] private List<GameObject> _pirates = new List<GameObject>();


    void Start()
    {
      EventManager.Current.OnChangedCurrentValue += OnChangedCurrentValue;
      _shipChanger.OnUpgradeShipEvent += OnShipUpgrade;
      _shipChanger.OnDowngradeShipEvent += OnShipDowngrade;
      FillPirates(pirateCounter.Count);
    }


    private void OnDestroy()
    {
      EventManager.Current.OnChangedCurrentValue -= OnChangedCurrentValue;
      _shipChanger.OnUpgradeShipEvent -= OnShipUpgrade;
      _shipChanger.OnDowngradeShipEvent -= OnShipDowngrade;
    }

    private void FillPirates(int newCountPirate)
    {
      if (_pirates.Count < newCountPirate)
      {
        for (int i = _pirates.Count; i < newCountPirate; i++)
        {
          Vector3 spawnPos = GetSpawnPos(i);
          AddPirate(spawnPos);
        }
      }
      else if (_pirates.Count > newCountPirate)
      {
        for (int i = _pirates.Count - 1; i > newCountPirate - 1; i--)
        {
          RemovePirate(i);
        }
      }
    }

    private Vector3 GetSpawnPos(int index)
    {
      Vector3 spawnPos = _startSpawnPos.localPosition;
      //Вычисляем остаток от индекса
      int indexX = index % _countInRow;
      //Вычисляем позицию по Х
      spawnPos.x += _deltaX * indexX;

      //Вычисляем целое от деления индекса на кол-во в ряду
      int indexZ = index / _countInRow;
      //Вычисляем позицию по Z
      spawnPos.z -= _deltaZ * indexZ;

      return spawnPos;
    }

    private void AddPirate(Vector3 spawnPos)
    {
      GameObject pirate = Instantiate(_prefabPirate, _parentForPirate);
      if (_prefabPirate.TryGetComponent(out Rigidbody rigidbody))
        rigidbody.isKinematic = true;
      pirate.transform.localPosition = spawnPos;
      _pirates.Add(pirate);
    }

    private void RemoveAllPirate()
    {
      int count = _pirates.Count;
      for (int index = count - 1; index >= 0; index--)
      {
        GameObject obj = _pirates[index].gameObject;
        _pirates.Remove(_pirates[index]);
        Destroy(obj);
      }
    }

    private void RemovePirate(int index)
    {
      GameObject obj = _pirates[index].gameObject;
      _pirates.Remove(_pirates[index]);
      obj.transform.SetParent(null);

      ThrowPirateToWater(index, obj);

      Destroy(obj, 10f);
    }

    private void ThrowPirateToWater(int index, GameObject obj)
    {
      if (obj.TryGetComponent(out Pirate pirate))
      {
        if (pirate.TryGetComponent(out Rigidbody rigidbody))
          rigidbody.isKinematic = false;
        pirate.DirectionForce = GetDirectionForce(index);
        pirate.GetForceAfterDeath();
      }
    }

    private Vector3 GetDirectionForce(int index)
    {
      Vector3 directionForce = Vector3.zero;
      directionForce += transform.up;

      //Вычисляем позицию пирата по линии
      int indexX = index % _countInRow;
      //Проверяем в какой стороне сидит пират
      if (indexX >= _countInRow / 2)
        directionForce += transform.right * _forceToSide;
      else
        directionForce -= transform.right * _forceToSide;

      return directionForce;
    }

    private void OnChangedCurrentValue(int currentValue)
    {
      _shipChanger.OnChangedCurrentValue(currentValue);
      if (gameObject.activeSelf)
        FillPirates(currentValue);
    }

    private void OnShipUpgrade()
    {
      RemoveAllPirate();
    }

    private void OnShipDowngrade()
    {
     // ThrowPirateToWater(0, _pirates[0]);
      RemoveAllPirate();
    }
  }
}