using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] private MMF_Player _damagePlayer;
  
  public void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private void Update()
  {
    //for testing, delete at the end
    if (Input.GetMouseButtonDown(1))
    {
      TakeDamageEffect();
    }
  }

  private void TakeDamageEffect()
  {
    _damagePlayer.PlayFeedbacks();
  }
}