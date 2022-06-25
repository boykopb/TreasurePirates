using System;
using UnityEngine;

namespace Managers
{
  public class EventManager : MonoBehaviour
  {
    [SerializeField] private AudioClip _onGameWinSfx;
    
    public Action<int> OnChangedCurrentValue;
    public Action<int> OnChangedCountPirate;

    public Action OnTutorial;
    public Action OnStartedGame;
    public Action OnGameWin;
    public Action OnGameLose;

    public static EventManager Current;

    private void Awake()
    {
      Current = this;
    }

 
    //On guide menu press anywhere on screen
    public void StartTutorial()
    {
      OnTutorial?.Invoke();
    }

    public void StartGame()
    {
      OnStartedGame?.Invoke();
    }

    public void GameWin()
    {
      OnGameWin?.Invoke();
      
      AudioManager.Instance.StopLevelMusic();
      AudioManager.Instance.PlaySFX(_onGameWinSfx, 1f, false);

    }

    public void GameLose()
    {
      OnGameLose?.Invoke();
      AudioManager.Instance.StopLevelMusic();
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