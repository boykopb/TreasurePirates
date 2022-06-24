using MoreMountains.Feedbacks;
using UnityEngine;

namespace Managers
{
  public class ScreenShaker : MonoBehaviour
  {
    [SerializeField] private MMF_Player _damageScreenStrongShakeEffect;
    [SerializeField] private MMF_Player _damageScreenLightShakeEffect;
    
    
    public static ScreenShaker Instance;
    

    private void Awake() =>
      Singleton();
    
    public void DoLightShake()
    {
      _damageScreenLightShakeEffect.PlayFeedbacks();
    }
    
    public void DoStrongShake()
    {
      _damageScreenStrongShakeEffect.PlayFeedbacks();
    }
    private void Singleton()
    {
      if (Instance == null)
        Instance = this;
      else
        Destroy(gameObject);
    }

  }
}