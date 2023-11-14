using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image icon;
    internal void Init(Ability ability)
    {
        this.ability = ability;
        icon.sprite = ability.GetIcon();
    }
    
    public void ActivateAbility()
    {
        ability.ActivateAbility();
    }
}
