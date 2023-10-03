using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private AimTargetingComponent aimTargetingComponent;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem fireVfx;
    private void Awake()
    {
        aimTargetingComponent = GetComponent<AimTargetingComponent>();
    }
    public override void Attack()
    {
        GameObject target = aimTargetingComponent.GetTarget();
        if (target != null)
        {
            DamageGameObject(target, damage);
        }

        fireVfx.Emit(fireVfx.emission.GetBurst(0).maxCount);
    }
}
