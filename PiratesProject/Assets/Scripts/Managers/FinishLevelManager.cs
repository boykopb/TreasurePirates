using UnityEngine;

public class FinishLevelManager : MonoBehaviour
{
    [SerializeField] private Transform _boatTransform;
    
    [SerializeField] private FinishLineTrigger _finishLine;
    [SerializeField] private ParticleSystem [] _onFinishConfettiVFX;
    [SerializeField] private Transform _stopBoatPoint;


    private Movement _boatMovement;
    
    private void Start()
    {
        _finishLine.OnFinishReachedEvent += OnFinishLineReach;

        _boatMovement = _boatTransform.GetComponent<Movement>();


    }

    private void OnDestroy()
    {
        _finishLine.OnFinishReachedEvent -= OnFinishLineReach;

    }

    private void OnFinishLineReach()
    {
        DisableBoatControl();
        PlayVFXConfetti();
    }

    private void DisableBoatControl()
    {
        _boatMovement.enabled = false;
    }

    private void PlayVFXConfetti()
    {
        for (int i = 0; i < _onFinishConfettiVFX.Length; i++) 
            _onFinishConfettiVFX[i].Play();
    }
    
    
    
    
}
