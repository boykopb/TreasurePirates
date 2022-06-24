using Managers;
using UnityEngine;

namespace Player
{
  public class ShipChangerVisualEffects : ObjectByCurveScaler
  {
    [SerializeField] private ShipChanger _shipChanger;

    [SerializeField] private AudioClip _upgradeSFX;
    [SerializeField] private GameObject _upgradeVFX;

    
  }
}