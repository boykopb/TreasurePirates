using UnityEngine;

namespace Player
{
    public class PirateForAnim : MonoBehaviour
    {
        [SerializeField] private Pirate _pirate;

        public void JumpFromBoat()
        {
            _pirate.GetForceAfterDeath();
        }
    }
}
