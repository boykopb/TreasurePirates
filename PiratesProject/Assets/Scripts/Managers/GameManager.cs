using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField] private AudioClip _onPauseSFX;
    
    private void Start()
    {
      Time.timeScale = 0;
    }

    public void StartTutorial()
    {
      EventManager.Current.StartTutorial();
    }


    public void StartGame()
    {
      Time.timeScale = 1;
      EventManager.Current.StartGame();
    }

    public void RestartLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void TogglePause()
    {
      Time.timeScale = Time.timeScale == 0 ? 1 : 0;
      AudioManager.Instance.PlaySFX(_onPauseSFX, 0f, false);
    }
  }
}