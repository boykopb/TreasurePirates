using Enemy;
using UnityEditor;
using UnityEngine;

namespace Managers
{
  [RequireComponent(typeof(Turrel))]
  public class GameObjectActivator : MonoBehaviour
  {
    [SerializeField] private float _activateDistance = 20f;

    private EnemyManager _enemyManager;
    private bool _isActive = true;
    private Turrel _turrel;
    
    private void Awake()
    {
      _turrel = GetComponent<Turrel>();
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
        //gameObject.SetActive(value);
        _turrel.enabled = value;
      }
    }

    private bool IsCloseTo(Vector3 target)
    {
      /*var toTarget = transform.position - target;
      var sqrtMagnitude = Vector3.SqrMagnitude(toTarget);
      
      return sqrtMagnitude < _activateDistance * _activateDistance;*/

      var distance = transform.position.z - target.z;
      return distance < _activateDistance && distance > -3;
    }
  }
}