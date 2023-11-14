using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWidget : MonoBehaviour
{
    Ability ability;
    internal void Init(Ability ability)
    {
        this.ability = ability;
    }
    
    public void ActivateAbility()
    {
        ability.ActivateAbility();
    }
}
