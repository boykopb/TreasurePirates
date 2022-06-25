using UnityEngine;

namespace Player
{
    public class Pirate : MonoBehaviour
    {
        public Vector3 DirectionForce { private get; set; }
        public bool IsDeath { private set; get; }

        [SerializeField] private float _forceJump = 10f;
        [SerializeField] private float _maxForceTorgue = 1000f;
        [SerializeField] private float _minForceTorgue = 100f;
        [SerializeField] private Animator _animator;
        private Rigidbody _rigidbody;
    
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = 1000f;
        }

        public void GetForceAfterDeath()
        {
            _rigidbody.AddForce(DirectionForce * _forceJump, ForceMode.Acceleration);
            //float randomXYZdirectionTorque = Random.Range(0f,1f);
            Vector3 directionTorque = new Vector3(
                Random.Range(0f,1f),
                Random.Range(0f,1f),
                Random.Range(0f,1f)
            );

            float randomForce = Random.Range(_minForceTorgue, _maxForceTorgue);
            _rigidbody.AddTorque(directionTorque * randomForce, ForceMode.Acceleration);
        }

        public void Death()
        {
            IsDeath = true;
        }
    }
}
