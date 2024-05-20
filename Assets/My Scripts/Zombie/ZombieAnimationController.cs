using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private ZombieBehaviour zombie;
    void Start()
    {
        zombie = GetComponentInParent<ZombieBehaviour>();
    }
    private void AttackTrigger()
    {
        zombie.DoDamagePlayer();
    }
    private void SetCanDespawn()
    {
        zombie.canDespawn = true;
    }
}
