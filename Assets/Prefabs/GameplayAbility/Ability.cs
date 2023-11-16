using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] float staminaCost;
    [SerializeField] float cooldownDuration;
    [SerializeField] Sprite icon;
    bool onCooldown = false;
    public event Action<float> onCooldownStarted;
    public Sprite GetIcon() { return icon; }

    public AbilityComponent OwningAblityComponet
    {
        get;
        private set;
    }

    internal void Init(AbilityComponent abilityComponent)
    {
        OwningAblityComponet = abilityComponent;
    }

    //return true if the ability is able to be casted
    //if returned true also commit it, meanning the stamina will be consumed, and the cooldown will start.
    public bool CommitAbility()
    {
        if (onCooldown)
            return false;

        if (!OwningAblityComponet.TryConsumeStamina(staminaCost))
            return false;

        StartCooldown();

        return true;
    }

    void StartCooldown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    public Coroutine StartCoroutine(IEnumerator enumerator)
    {
        return OwningAblityComponet.StartCoroutine(enumerator);
    }

    IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        onCooldownStarted?.Invoke(cooldownDuration);
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }

    public abstract void ActivateAbility();
}