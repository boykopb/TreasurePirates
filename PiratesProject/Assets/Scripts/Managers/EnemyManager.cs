using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Managers
{
  public class EnemyManager : MonoBehaviour
  {
    //check every N frame;
    private const int UpdateInterval = 10;

    private readonly List<GameObjectActivator> _enemiesOnScene = new List<GameObjectActivator>();
    private Transform _playerTransform;

    public event Action OnOneEnemyKilledEvent;
    public event Action OnAllEnemiesKilledEvent;
    
    private void Start()
    {
      _playerTransform = FindObjectOfType<Movement>().transform;
    }

    private void Update()
    {
      if (Time.frameCount % UpdateInterval != 0)
        return;

      for (var i = 0; i < _enemiesOnScene.Count; i++)
      {
        if (_playerTransform == null)
          break;
        _enemiesOnScene[i].ToggleObjectByDistanceTo(_playerTransform.position);
      }
    }

    public void AddToList(GameObjectActivator gameObjectActivator)
    {
      _enemiesOnScene.Add(gameObjectActivator);
    }

    public void RemoveFromList(GameObjectActivator gameObjectActivator)
    {
      _enemiesOnScene.Remove(gameObjectActivator);
      OnOneEnemyKilledEvent?.Invoke();
      
      if (_enemiesOnScene.Count == 0)
        OnAllEnemiesKilledEvent?.Invoke();
    }

    public int GetEnemyCount() => 
      _enemiesOnScene.Count;
  }
}