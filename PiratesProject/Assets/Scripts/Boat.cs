using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private GameObject _prefabPirate;
    [SerializeField] private int _countInRow = 2;
    [SerializeField] private float _deltaZ = 5f;
    [SerializeField] private float _deltaX = 5f;
    [SerializeField] private Transform _startSpawnPos;
    [SerializeField] private float _forceToSide = 10f;
    [Range(0, 15)][SerializeField] private int _testCount = 5;

    private List<GameObject> _pirates = new List<GameObject>();
    void Start()
    {
        EventManager.Current.OnChangedValue += OnChangedValue;
    }

    private void OnChangedValue(int newCountPirate)
    {
        //FillPirates(newCountPirate);
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
        else if(_pirates.Count > newCountPirate)
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
        GameObject pirate = Instantiate(_prefabPirate, transform);
        pirate.transform.localPosition = spawnPos;
        _pirates.Add(pirate);
    }

    private void RemovePirate(int index)
    {
        GameObject obj = _pirates[index].gameObject;
        _pirates.Remove(_pirates[index]);
        obj.transform.SetParent(null);
        
        if (obj.TryGetComponent(out Pirate pirate))
        {
            pirate.GetForceAfterDeath(GetDirectionForce(index));
        }
        Destroy(obj,10f);
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
    
    void Update()
    {
        FillPirates(_counter.CountPirates);
    }
}
