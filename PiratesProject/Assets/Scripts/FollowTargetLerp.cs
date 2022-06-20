using UnityEngine;

public class FollowTargetLerp : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate = 5f;

    private void Update()
    {
        if (_target == null)
            return;
      
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);
    }
}
