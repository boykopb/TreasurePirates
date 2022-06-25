using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Player
{
  public class BoatPassengers : MonoBehaviour
  {
    [Header("Ship Settings")]
    [SerializeField] private PirateCounter _pirateCounter;
    [SerializeField] private ShipChanger _shipChanger;
    [SerializeField] private Transform[] _startPositionOnShipTransform;
   
    [Header("Pirate Settings")]
    [SerializeField] private GameObject _prefabPirate;
    [SerializeField] private float _forceToSide = 2f;
    [SerializeField] private float _forceZ = 1f;
    
    private List<GameObject> _pirates = new List<GameObject>();
    private List<BoatSeatsInfo> _boatSeatsInfos;

    void Start()
    {
      _boatSeatsInfos = new List<BoatSeatsInfo>();
      _boatSeatsInfos.Add(new BoatSeatsInfo(2, 0.5f, 0f));
      _boatSeatsInfos.Add(new BoatSeatsInfo(2, 0.45f, 0.8f));
      _boatSeatsInfos.Add(new BoatSeatsInfo(2, 0.5f, 0.5f));


      EventManager.Current.OnChangedCurrentValue += OnChangedCurrentValue;
      _shipChanger.OnUpgradeShipEvent += OnShipUpgrade;
      _shipChanger.OnDowngradeShipEvent += OnShipDowngrade;
      FillPirates(_pirateCounter.Count);
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
      var currentShipIndex = _shipChanger.GetCurrentShipIndex();
      var currentBoat = _boatSeatsInfos[currentShipIndex];
      Vector3 spawnPos = _startPositionOnShipTransform[currentShipIndex].localPosition;
      //Вычисляем остаток от индекса
      int indexX = index % currentBoat.CountInRow;
      //Вычисляем позицию по Х
      spawnPos.x += currentBoat.DeltaX * indexX;

      //Вычисляем целое от деления индекса на кол-во в ряду
      int indexZ = index / currentBoat.CountInRow;
      //Вычисляем позицию по Z
      spawnPos.z -= currentBoat.DeltaZ * indexZ;

      return spawnPos;
    }

    private void AddPirate(Vector3 spawnPos)
    {
      GameObject pirate = Instantiate(_prefabPirate, transform);
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

      obj.GetComponent<Pirate>().Death();
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
      var currentShipIndex = _shipChanger.GetCurrentShipIndex();
      var currentBoat = _boatSeatsInfos[currentShipIndex];

      Vector3 directionForce = transform.forward * _forceZ;
      directionForce += transform.up;

      //Вычисляем позицию пирата по линии
      int indexX = index % currentBoat.CountInRow;
      //Проверяем в какой стороне сидит пират
      if (indexX >= currentBoat.CountInRow / 2)
        directionForce += transform.right;
      else
        directionForce -= transform.right;
      directionForce *= _forceToSide;

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
      RemovePirate(_pirates.Count - 1);
      RemoveAllPirate();
    }
  }
}