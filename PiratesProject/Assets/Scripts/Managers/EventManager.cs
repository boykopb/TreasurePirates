using System;
using UnityEngine;

namespace Managers
{
  public class EventManager : MonoBehaviour
  {
    public Action<int> OnChangedCurrentValue;
    public Action<int> OnChangedCountPirate;
    public Action OnShipChanged;
    public Action OnGameLose;
    public Action OnGameWin;
    public Action OnStartedGame;

    public static EventManager Current;

    private void Awake()
    {
      Current = this;
    }

    public void GameLose()
    {
      OnGameLose?.Invoke();
    }

    public void GameWin()
    {
      OnGameWin?.Invoke();
    }

    public void StartGame()
    {
      OnStartedGame?.Invoke();
    }

    public void ChangedCurrentValue(int currentValue)
    {
      OnChangedCurrentValue?.Invoke(currentValue);
    }

    public void ChangedCountPirate(int value)
    {
      if (value < 0) ScreenShaker.Instance.DoStrongShake();

      OnChangedCountPirate?.Invoke(value);
    }
  }
}