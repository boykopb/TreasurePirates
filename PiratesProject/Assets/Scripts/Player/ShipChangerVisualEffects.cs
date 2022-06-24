using Managers;
using UnityEngine;

namespace Player
{
  public class ShipChangerVisualEffects : ObjectByCurveScaler
  {
    [SerializeField] private ShipChanger _shipChanger;

    [SerializeField] private AudioClip _upgradeSFX;
    [SerializeField] private GameObject _upgradeVFX;

    private protected override void Start()
    {
      _shipChanger.OnUpgradeShipEvent += OnUpgrade;
      _shipChanger.OnDowngradeShipEvent += ChangeScale;
    }


    private void OnUpgrade()
    {
      ChangeScale();
      PlayVFX(_upgradeVFX);
      AudioManager.Instance.PlaySFX(_upgradeSFX);
    }

    

    private void PlayVFX(GameObject vfx)
    {
      var position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
      Instantiate(vfx, position, Quaternion.Euler(new Vector3(-80, 90, 0)));
      Instantiate(vfx, position, Quaternion.Euler(new Vector3(-105, 90, 0)));
    }
  }
}