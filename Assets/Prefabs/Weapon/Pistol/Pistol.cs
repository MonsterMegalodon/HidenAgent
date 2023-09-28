using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RangedWeapon
{
    private AimTargetingComponent aimTargetingComponent;

    private void Awake()
    {
        aimTargetingComponent = GetComponent<AimTargetingComponent>();
    }

    public override void Attack()
    {
        GameObject target = aimTargetingComponent.GetTraget();
        if (target != null)
        {
            Debug.Log($"attacking : {target.name}");
        }
    }
}
