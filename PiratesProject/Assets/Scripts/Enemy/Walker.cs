using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
  public enum Direction
  {
    Left,
    Right
  }

  public class Walker : MonoBehaviour
  {
    //[SerializeField] private Animator _animator;
    //[SerializeField] private Transform _rayCastPoint;
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private Direction _currentDirection = Direction.Left;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopTime = 2f;

    private bool _isStopped;
    public UnityEvent OnLeftTargetReachedEvent;
    public UnityEvent OnRightTargetReachedEvent;

    private void Start()
    {
      _leftTarget.parent = null;
      _rightTarget.parent = null;
    }

    private void Update()
    {
      if (_isStopped)
        return;

      WalkBetweenTargets();
      PlaceTransformOnGround();
    }

    private void PlaceTransformOnGround()
    {
      //if (Physics.Raycast(_rayCastPoint.position, Vector3.down, out var hit))
        //transform.position = hit.point;
    }

    private void WalkBetweenTargets()
    {
      if (_currentDirection == Direction.Left)
      {
        transform.position -= GetNewPosition();

        if (IsLeftTargetReached()) 
          StartCoroutine(StopWaitTurnAndGo(Direction.Right));
      }
      else
      {
        transform.position += GetNewPosition();

        if (IsRightTargetReached()) 
          StartCoroutine(StopWaitTurnAndGo(Direction.Left));
      }
    }

    private Vector3 GetNewPosition()
    {
      return new Vector3(Time.deltaTime * _moveSpeed, 0f, 0f);
    }

    private bool IsRightTargetReached()
    {
      return transform.position.x > _rightTarget.position.x;
    }

    private bool IsLeftTargetReached()
    {
      return transform.position.x < _leftTarget.position.x;
    }
    
    
    private IEnumerator StopWaitTurnAndGo(Direction direction)
    {
      _isStopped = true;
      //_animator.enabled = false;
      yield return new WaitForSeconds(_stopTime);
      ChangeDirectionTo(direction);
      _isStopped = false;
      //_animator.enabled = true;

    }

    private void ChangeDirectionTo(Direction direction)
    {
      _currentDirection = direction;

      if (direction == Direction.Right)
        OnLeftTargetReachedEvent?.Invoke();
      else
        OnRightTargetReachedEvent?.Invoke();
    }
  }
}