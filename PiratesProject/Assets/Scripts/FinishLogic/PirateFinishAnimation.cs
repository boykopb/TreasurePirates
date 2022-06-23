using System;
using UnityEngine;

public enum FinishPose
{
  Pose1,
  Pose2,
  Pose3,
  Pose4,
  Pose5,
  Pose6,
  Pose7
}
public class PirateFinishAnimation : MonoBehaviour
{
  
  [SerializeField] private FinishPose _finishPose;

  private Animator _animator;

  

  private void Start()
  {
    _animator = GetComponent<Animator>();

    switch (_finishPose)
    {
      case FinishPose.Pose1:
        break;
      case FinishPose.Pose2:
        _animator.SetTrigger(FinishPose.Pose2.ToString());
        break;
      case FinishPose.Pose3:
        _animator.SetTrigger(FinishPose.Pose3.ToString());
        break;
      case FinishPose.Pose4:
        _animator.SetTrigger(FinishPose.Pose4.ToString());
        break;
      case FinishPose.Pose5:
        _animator.SetTrigger(FinishPose.Pose5.ToString());
        break;
      case FinishPose.Pose6:
        _animator.SetTrigger(FinishPose.Pose6.ToString());
        break;
      case FinishPose.Pose7:
        _animator.SetTrigger(FinishPose.Pose7.ToString());
        break;
    }
  }
}