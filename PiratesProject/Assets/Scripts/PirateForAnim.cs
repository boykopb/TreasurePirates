using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateForAnim : MonoBehaviour
{
    [SerializeField] private Pirate _pirate;

    public void JumpFromBoat()
    {
        _pirate.GetForceAfterDeath();
    }
}
