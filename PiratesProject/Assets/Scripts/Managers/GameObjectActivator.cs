using UnityEditor;
using UnityEngine;

namespace Managers
{
  public class GameObjectActivator : MonoBehaviour
  {
    [SerializeField] private float _activateDistance = 20f;

    private EnemyManager _enemyManager;
    private bool _isActive = true;

    private void Awake()
    {
      _enemyManager = FindObjectOfType<EnemyManager>();
      _enemyManager.AddToList(this);
    }

    private void OnDestroy()
    {
      _enemyManager.RemoveFromList(this);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
      Handles.color = Color.gray;
      Handles.DrawWireDisc(transform.position, Vector3.forward, _activateDistance);
    }
#endif

    public void ToggleObjectByDistanceTo(Vector3 target)
    {
      SetActive(IsCloseTo(target));
    }

    private void SetActive(bool value)
    {
      if (_isActive != value)
      {
        _isActive = value;
        gameObject.SetActive(value);
      }
    }

    private bool IsCloseTo(Vector3 target)
    {
      var toTarget = transform.position - target;
      var sqrtMagnitude = Vector3.SqrMagnitude(toTarget);
      
      return sqrtMagnitude < _activateDistance * _activateDistance;
    }
  }
}