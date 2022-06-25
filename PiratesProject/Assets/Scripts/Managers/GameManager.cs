using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
  public class GameManager : MonoBehaviour
  {
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
  }
}